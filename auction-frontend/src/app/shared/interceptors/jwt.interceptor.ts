import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services';
import { mergeMap } from 'rxjs';


export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  
  const token = authService.getAccessToken();

  if (!token || req.url.endsWith(`/identity/refresh`)) {
    return next(req);
  }

  if (!authService.isAccessTokenExprired(token)) {
    return next(
      req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      })
    );
  }

  return authService.refreshToken().pipe(
    mergeMap((token) =>
      next(
        req.clone({
          headers: req.headers.set('Authorization', `Bearer ${token}`)
        })
      )
    )
  );
};
