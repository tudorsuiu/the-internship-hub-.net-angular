import { CommonModule, Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ICompany } from '../dtos/ICompany';
import { UserService } from '../services/user/user.service';
import { IUser } from '../dtos/IUser';

@Component({
    selector: 'app-add-internship',
    templateUrl: './add-internship.component.html',
    styleUrl: './add-internship.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class AddInternshipComponent {
    title: string = '';
    description: string = '';
    company: ICompany | null = null;
    startDate: Date | null = null;
    endDate: Date | null = null;
    positionsAvailable: number | null = null;
    compensation: number | null = null;

    constructor(private location: Location, private userService: UserService) {}

    ngOnInit() {
        this.userService.getUserById(localStorage.getItem('token')).subscribe(
            (response: IUser) => {
                console.log(response);
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    navigateBack() {
        this.location.back();
    }

    addInternship() {
        console.log(this.startDate);
    }
}
