import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
import { IUserLogin } from '../dtos/IUserLogin';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrl: './login.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class LoginComponent {
    hidePassword: boolean = true;
    password: string = '';
    email: string = '';
    errorMessage: string = '';

    constructor(private router: Router, private authService: AuthService) {}

    ngOnInit(): void {}

    togglePasswordVisibility(): void {
        this.hidePassword = !this.hidePassword;
    }

    login() {
        var userLogin: IUserLogin = {
            email: this.email,
            password: this.password,
        };
        this.authService.login(userLogin).subscribe(
            (response: any) => {
                this.clearLocalStorage();
                localStorage.setItem('token', response);
                this.router.navigate(['/homepage']);
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    clearLocalStorage(): void {
        localStorage.clear();
    }
}
