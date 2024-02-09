import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable, map, switchMap } from 'rxjs';
import { CreateUser, User } from '../models';
import { jwtDecode } from 'jwt-decode';

const prefix = 'AUCTION'

enum LocalStorageAuthKeys {
  User = `${prefix}_USER`,
  AccessToken = `${prefix}_ACCESS_TOKEN`,
  RefreshToken = `${prefix}_REFRESH_TOKEN`,
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  http = inject(HttpClient);
  baseUrl = `${environment.baseUrl}/identity`;

  private currentUser$ = new BehaviorSubject<User | null>(null);

  getCurrentUser$() : Observable<User | null> {
    if (this.currentUser$.value === null) {
      this.tryGetUser();
    }

    return this.currentUser$.asObservable();
  }

  private tryGetUser() : void {
    const userStr = localStorage.getItem(LocalStorageAuthKeys.User);
    if (userStr) {
      this.currentUser$.next(JSON.parse(userStr));
    }
  }

  getAccessToken() : string | null {
    return localStorage.getItem(LocalStorageAuthKeys.AccessToken);
  }

  refreshToken() : Observable<string> {
    const refreshToken = localStorage.getItem(LocalStorageAuthKeys.RefreshToken);
    const accessToken = this.getAccessToken();

    return this.http.post<{ refreshToken: string }>(`${this.baseUrl}/refresh`, {
      accessToken,
      refreshToken
    }).pipe(map((newAccessToken) => {
      localStorage.setItem(LocalStorageAuthKeys.AccessToken, newAccessToken.refreshToken);

      return newAccessToken.refreshToken;
    }))
  }

  isAccessTokenExprired(token: string) : boolean {
    const decoded = jwtDecode(token);
    return Date.now() / 1000 > decoded.exp!;
  }

  registerUser(createUser: CreateUser) {
    return this.http.post(`${this.baseUrl}/register`, createUser).pipe(
      switchMap(() => this.signIn(createUser.email, createUser.password))
    );
  }

  signIn(emailOrUsername: string, password: string) : Observable<string> {
    return this.http.post<{ accessToken: string, refreshToken: string }>(`${this.baseUrl}/login`, {
      emailOrUsername,
      password
    }).pipe(
      map(({accessToken, refreshToken}) => {
        localStorage.setItem(LocalStorageAuthKeys.AccessToken, accessToken);
        localStorage.setItem(LocalStorageAuthKeys.RefreshToken, refreshToken);

        this.setUser(accessToken);

        return accessToken;
      }),
    );
  }

  private setUser(token: string) {
    const decoded = jwtDecode(token);

    const user = {} as unknown as User;

    Object.entries(decoded).forEach(([key, value]) => {
      if (key.endsWith('emailaddress')) {
        user['email'] = value;
      }

      if (key.endsWith('nameidentifier')) {
        user['id'] = value;
      }

      if (key.endsWith('name')) {
        user['username'] = value;
      }
    })

    this.currentUser$.next(user);
    localStorage.setItem(LocalStorageAuthKeys.User, JSON.stringify(user));
  }

  signOut() {
    if (!this.currentUser$.value) {
      return;
    }

    this.http.post(`${this.baseUrl}/logout`, {}).subscribe();

    localStorage.removeItem(LocalStorageAuthKeys.AccessToken);
    localStorage.removeItem(LocalStorageAuthKeys.RefreshToken);
    localStorage.removeItem(LocalStorageAuthKeys.User);
    
    this.currentUser$.next(null);
  }
}
