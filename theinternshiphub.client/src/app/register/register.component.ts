import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { IUserRegister, Role } from '../dtos/IUserRegister';
import { AuthService } from '../services/auth/auth.service';
import { CompanyService } from '../services/company/company.service';
import { ICompany } from '../dtos/ICompany';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class RegisterComponent {
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    phoneNumber: string = '';
    city: string = '';
    selectedCompany: string = '';
    password: string = '';
    role: Role | null = Role.Student;
    roles = Object.values(Role);

    companies: ICompany[] = [];

    errorMessage: string = '';

    constructor(
        private router: Router,
        private authService: AuthService,
        private companyService: CompanyService
    ) {}

    ngOnInit() {
        this.companyService.getUniversities().subscribe(
            (response: ICompany[]) => {
                this.companies = response;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    public register(userForm: NgForm) {
        var registerUser: IUserRegister = {
            firstName: this.firstName,
            lastName: this.lastName,
            email: this.email,
            phoneNumber: this.phoneNumber,
            companyId: this.selectedCompany,
            password: this.password,
            role: this.role,
            city: this.city,
        };

        this.authService.register(registerUser).subscribe(
            (response: any) => {
                this.router.navigate(['/login']);
            },
            (error: any) => {
                console.log(error.message);
            }
        );

        this.resetForm(userForm);
    }

    public resetForm(userForm: NgForm) {
        userForm.resetForm();
    }
}
