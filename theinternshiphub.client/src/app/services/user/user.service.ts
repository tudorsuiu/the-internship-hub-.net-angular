import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from '../../dtos/IUser';
import { endpointAPI } from '../../../config';

@Injectable({
    providedIn: 'root',
})
export class UserService {
    constructor(private http: HttpClient) {}

    getUserById(): Observable<IUser> {
        return this.http.get<IUser>(`${endpointAPI}/api/Users/logged-user`);
    }
}
