import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { endpointAPI } from '../../../config';
import { IInternship } from '../../dtos/IInternship';
import { IInternshipAdd } from '../../dtos/IInternshipAdd';

@Injectable({
    providedIn: 'root',
})
export class InternshipService {
    constructor(private http: HttpClient) {}

    getInternships(): Observable<IInternship[]> {
        return this.http.get<IInternship[]>(`${endpointAPI}/api/Internships`);
    }

    addInternship(internship: IInternshipAdd) {
        return this.http.post(`${endpointAPI}/api/Internships`, internship);
    }

    deleteInternship(internshipId: string) {
        return this.http.delete(
            `${endpointAPI}/api/Internships/${internshipId}`
        );
    }

    getInternshipById(internshipId: string): Observable<IInternship> {
        return this.http.get<IInternship>(
            `${endpointAPI}/api/Internships/${internshipId}`
        );
    }

    updateInternship(internship: IInternship) {
        return this.http.put(`${endpointAPI}/api/Internships`, internship);
    }
}
