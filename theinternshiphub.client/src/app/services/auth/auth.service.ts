import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IUserLogin } from '../../dtos/IUserLogin';
import { endpointAPI } from '../../../config';
import { IUserRegister } from '../../dtos/IUserRegister';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private http: HttpClient) {}

    login(user: IUserLogin) {
        return this.http.post(`${endpointAPI}/api/Authentication/login`, user, {
            responseType: 'text',
        });
    }

    register(user: IUserRegister) {
        return this.http.post<boolean>(
            `${endpointAPI}/api/Authentication/register`,
            user
        );
    }
}
