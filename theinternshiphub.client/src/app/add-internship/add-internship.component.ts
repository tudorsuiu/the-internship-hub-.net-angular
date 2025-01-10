import { CommonModule, Location } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ICompany } from '../dtos/ICompany';
import { UserService } from '../services/user/user.service';
import { IUser } from '../dtos/IUser';
import { InternshipService } from '../services/internship/internship.service';
import { Router } from '@angular/router';
import { IInternshipAdd } from '../dtos/IInternshipAdd';

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
    deadline: Date | null = null;
    domain: string = '';

    user: IUser | null = null;

    constructor(
        private location: Location,
        private userService: UserService,
        private internshipService: InternshipService,
        private router: Router
    ) {}

    ngOnInit() {
        this.userService.getLoggedUSer().subscribe(
            (response: IUser) => {
                this.user = response;
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
        var internship: IInternshipAdd = {
            title: this.title,
            description: this.description,
            company: this.user?.company,
            recruiter: this.user,
            startDate: this.startDate,
            endDate: this.endDate,
            positionsAvailable: this.positionsAvailable,
            compensation: this.compensation,
            isDeleted: false,
            deadline: this.deadline,
            domain: this.domain,
        };
        this.internshipService.addInternship(internship).subscribe(
            (response: any) => {
                this.router.navigate(['/company']);
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }
}
