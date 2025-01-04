import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { endpointAPI } from '../../../config';
import { Observable } from 'rxjs';
import { Company } from '../../dtos/Company';

@Injectable({
    providedIn: 'root',
})
export class CompanyService {
    constructor(private http: HttpClient) {}

    getUniversities(): Observable<Company[]> {
        return this.http.get<Company[]>(
            `${endpointAPI}/api/Companies/register`,
            {}
        );
    }
}
