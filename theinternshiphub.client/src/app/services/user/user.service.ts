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

    getLoggedUSer(): Observable<IUser> {
        return this.http.get<IUser>(`${endpointAPI}/api/Users/logged-user`);
    }

    updateUser(user: IUser) {
        return this.http.put(`${endpointAPI}/api/Users`, user);
    }

    deleteUser(userId: string) {
        return this.http.delete(`${endpointAPI}/api/Users/${userId}`);
    }
}
