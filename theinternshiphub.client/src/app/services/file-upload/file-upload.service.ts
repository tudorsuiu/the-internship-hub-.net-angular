import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { endpointAPI } from '../../../config';

@Injectable({
    providedIn: 'root',
})
export class FileUploadService {
    constructor(private http: HttpClient) {}

    uploadFile(file: File): Observable<any> {
        const formData = new FormData();
        formData.append('file', file);

        return this.http.post(`${endpointAPI}/api/Files/upload`, formData);
    }
}
