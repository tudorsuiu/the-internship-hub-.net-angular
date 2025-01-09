import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IApplication } from '../../dtos/IApplication';
import { endpointAPI } from '../../../config';
import { ICandidateProfileDTO } from '../../dtos/CandidateProfile/ICandidateProfileDTO';

@Injectable({
    providedIn: 'root',
})
export class ApplicationService {
    constructor(private http: HttpClient) {}

    getApplications(): Observable<IApplication[]> {
        return this.http.get<IApplication[]>(`${endpointAPI}/api/Applications`);
    }

    updateApplication(application: IApplication) {
        return this.http.put(`${endpointAPI}/api/Applications`, application);
    }

    deleteApplication(applicationId: string) {
        return this.http.delete(
            `${endpointAPI}/api/Applications/${applicationId}`
        );
    }

    addApplication(application: IApplication) {
        return this.http.post(`${endpointAPI}/api/Applications`, application);
    }

    getApplicationById(applicationId: string): Observable<IApplication> {
        return this.http.get<IApplication>(
            `${endpointAPI}/api/Applications/${applicationId}`
        );
    }

    getCandidateProfile(applicationId: string) {
        return this.http.get<ICandidateProfileDTO>(
            `${endpointAPI}/api/OpenAI/${applicationId}`
        );
    }
}
