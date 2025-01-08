import { CommonModule, Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user/user.service';
import { IUser } from '../dtos/IUser';
import { ICompany } from '../dtos/ICompany';
import { Role } from '../dtos/IUserRegister';

@Component({
    selector: 'app-myaccount',
    templateUrl: './myaccount.component.html',
    styleUrl: './myaccount.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class MyaccountComponent {
    userId: string = '';
    firstName: string = '';
    lastName: string = '';
    email: string = '';
    phoneNumber: string = '';
    company: ICompany = {
        cO_ID: '',
        cO_NAME: '',
        cO_CITY: '',
        cO_WEBSITE: '',
        cO_LOGO_PATH: '',
    };
    role: Role | null = null;
    city: string = '';

    constructor(
        private location: Location,
        private router: Router,
        private userService: UserService
    ) {}

    navigateBack() {
        this.location.back();
    }

    ngOnInit() {
        this.userService.getLoggedUSer().subscribe(
            (response: IUser) => {
                this.userId = response.id;
                this.firstName = response.firstName;
                this.lastName = response.lastName;
                this.email = response.email;
                this.phoneNumber = response.phoneNumber;
                this.company = response.company;
                this.role = response.role;
                this.city = response.city;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    updateUser() {
        var user: IUser = {
            id: this.userId,
            firstName: this.firstName,
            lastName: this.lastName,
            email: this.email,
            phoneNumber: this.phoneNumber,
            company: this.company,
            role: this.role,
            city: this.city,
            isDeleted: false,
        };
        this.userService.updateUser(user).subscribe(
            (response: any) => {
                if (this.role === 'Student') {
                    this.router.navigate(['/homepage']);
                } else if (this.role === 'Recruiter') {
                    this.router.navigate(['/company']);
                } else {
                    this.router.navigate(['/university']);
                }
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    deleteAccount() {
        this.userService.deleteUser(this.userId).subscribe(
            (response: any) => {
                alert('Account deleted');
                this.router.navigate(['/login']);
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }
}
