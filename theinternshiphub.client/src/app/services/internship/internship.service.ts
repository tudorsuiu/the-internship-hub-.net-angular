import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IInternship } from '../../dtos/IInternship';
import { endpointAPI } from '../../../config';

@Injectable({
    providedIn: 'root',
})
export class InternshipService {
    constructor(private http: HttpClient) {}

    getInternships(): Observable<IInternship[]> {
        return this.http.get<IInternship[]>(`${endpointAPI}/api/Internships`);
    }
}
