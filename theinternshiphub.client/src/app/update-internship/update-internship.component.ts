import { Component } from '@angular/core';
import { IInternship } from '../dtos/IInternship';
import { ActivatedRoute } from '@angular/router';
import { InternshipService } from '../services/internship/internship.service';
import { CommonModule, DatePipe, formatDate, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-update-internship',
    templateUrl: './update-internship.component.html',
    styleUrl: './update-internship.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class UpdateInternshipComponent {
    internshipId: string | null = null;
    internship: IInternship = {
        id: '',
        title: '',
        description: '',
        company: {
            cO_ID: '',
            cO_NAME: '',
            cO_CITY: '',
            cO_WEBSITE: '',
            cO_LOGO_PATH: '',
        },
        recruiter: {
            id: '',
            firstName: '',
            lastName: '',
            email: '',
            phoneNumber: '',
            company: {
                cO_ID: '',
                cO_NAME: '',
                cO_CITY: '',
                cO_WEBSITE: '',
                cO_LOGO_PATH: '',
            },
            role: null,
            city: '',
            isDeleted: true,
        },
        startDate: new Date(),
        endDate: new Date(),
        positionsAvailable: 0,
        compensation: 0,
        isDeleted: false,
        deadline: new Date(),
    };

    title: string = '';
    description: string = '';
    startDate: string = '';
    endDate: string = '';
    positionsAvailable: number = 0;
    compensation: number = 0;
    isDeleted: string = '';
    deadline: string = '';

    constructor(
        private route: ActivatedRoute,
        private internshipService: InternshipService,
        private location: Location
    ) {}

    ngOnInit(): void {
        this.internshipId = this.route.snapshot.paramMap.get('id');
        if (this.internshipId) {
            this.internshipService
                .getInternshipById(this.internshipId)
                .subscribe(
                    (response: IInternship) => {
                        this.internship = response;
                        this.title = response.title;
                        this.description = response.description;

                        this.startDate = formatDate(
                            new Date(
                                response.startDate.toString().slice(0, 10)
                            ),
                            'yyyy-MM-dd',
                            'en-US'
                        );

                        this.endDate = formatDate(
                            new Date(response.endDate.toString().slice(0, 10)),
                            'yyyy-MM-dd',
                            'en-US'
                        );
                        this.positionsAvailable = response.positionsAvailable;
                        this.compensation = response.compensation;
                        this.isDeleted = String(response.isDeleted);
                        this.deadline = formatDate(
                            new Date(response.deadline.toString().slice(0, 10)),
                            'yyyy-MM-dd',
                            'en-US'
                        );
                    },
                    (error: any) => {
                        console.log(error.message);
                    }
                );
        }
    }

    navigateBack() {
        this.location.back();
    }

    updateInternship() {
        var internship: IInternship = {
            id: this.internship.id,
            title: this.title,
            description: this.description,
            company: {
                cO_ID: this.internship.company.cO_ID,
                cO_NAME: this.internship.company.cO_NAME,
                cO_CITY: this.internship.company.cO_CITY,
                cO_WEBSITE: this.internship.company.cO_WEBSITE,
                cO_LOGO_PATH: this.internship.company.cO_LOGO_PATH,
            },
            recruiter: {
                id: this.internship.recruiter.id,
                firstName: this.internship.recruiter.firstName,
                lastName: this.internship.recruiter.lastName,
                email: this.internship.recruiter.email,
                phoneNumber: this.internship.recruiter.phoneNumber,
                company: {
                    cO_ID: this.internship.recruiter.company.cO_ID,
                    cO_NAME: this.internship.recruiter.company.cO_NAME,
                    cO_CITY: this.internship.recruiter.company.cO_CITY,
                    cO_WEBSITE: this.internship.recruiter.company.cO_WEBSITE,
                    cO_LOGO_PATH:
                        this.internship.recruiter.company.cO_LOGO_PATH,
                },
                role: this.internship.recruiter.role,
                city: this.internship.recruiter.city,
                isDeleted: this.internship.recruiter.isDeleted,
            },
            startDate: new Date(this.startDate),
            endDate: new Date(this.endDate),
            positionsAvailable: this.positionsAvailable,
            compensation: this.compensation,
            isDeleted: this.isDeleted.toLowerCase() == 'true',
            deadline: new Date(this.deadline),
        };

        this.internshipService.updateInternship(internship).subscribe(
            (response: any) => {
                alert('Internship post updated succesfully');
                this.navigateBack();
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }
}
