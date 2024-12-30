import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throwError } from 'rxjs';
import { LoginResponse } from '../../../dtos/LoginResponse';
import { endpointAPI } from '../../../config';
import { IUserLogin } from '../../../dtos/IUserLogin';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private http: HttpClient) {}

    login(userLogin: IUserLogin): Observable<LoginResponse> {
        console.log('Login called');
        return this.http
            .post<LoginResponse>(`${endpointAPI}/api/authentication/login`, {
                userLogin,
            })
            .pipe(
                catchError(error => {
                    console.log('Login error:', error);
                    return throwError(
                        () => new Error('Something went wrong during login.')
                    );
                })
            );
    }
}
