import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { endpointAPI } from '../../../config';
import { Observable } from 'rxjs';
import { ICompany } from '../../dtos/ICompany';

@Injectable({
    providedIn: 'root',
})
export class CompanyService {
    constructor(private http: HttpClient) {}

    getUniversities(): Observable<ICompany[]> {
        return this.http.get<ICompany[]>(
            `${endpointAPI}/api/Companies/register`,
            {}
        );
    }
}
