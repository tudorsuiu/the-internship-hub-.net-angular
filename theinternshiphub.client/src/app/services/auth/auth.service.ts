import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserLogin } from '../../dtos/IUserLogin';
import { endpointAPI } from '../../../config';
import { IUserRegister } from '../../dtos/IUserRegister';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private http: HttpClient) {}

    login(user: IUserLogin): Observable<any> {
        return this.http.post(`${endpointAPI}/api/Authentication/login`, user);
    }

    register(user: IUserRegister) {
        return this.http.post<boolean>(
            `${endpointAPI}/api/Authentication/register`,
            user
        );
    }
}
