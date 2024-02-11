import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { StaticFile } from '../models';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  http = inject(HttpClient)
  baseUrl = `${environment.baseUrl}/staticfile`

  constructor() { }

  uploadFile(files: File[]) {
    const formData = new FormData();
    files.forEach((file) => {
      formData.append("files", file);
    })
    return this.http.post<StaticFile[]>(this.baseUrl, formData);
  }
}
