import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Role } from '../dtos/IUserRegister';

@Component({
    selector: 'app-details-page',
    templateUrl: './details-page.component.html',
    styleUrl: './details-page.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class DetailsPageComponent {
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

    constructor(
        private route: ActivatedRoute,
        private router: Router,
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
                        console.log(this.internship);
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
}
