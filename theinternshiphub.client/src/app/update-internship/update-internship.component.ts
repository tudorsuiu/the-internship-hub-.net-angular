import { Component } from '@angular/core';
import { IInternship } from '../dtos/IInternship';
import { ActivatedRoute } from '@angular/router';
import { InternshipService } from '../services/internship/internship.service';
import { CommonModule, DatePipe, Location } from '@angular/common';
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
    startDate: Date = new Date();
    endDate: Date = new Date();
    positionsAvailable: number = 0;
    compensation: number = 0;
    isDeleted = false;
    deadline: Date = new Date();

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
                        this.startDate = response.startDate;
                        this.endDate = response.endDate;
                        this.positionsAvailable = response.positionsAvailable;
                        this.compensation = response.compensation;
                        this.isDeleted = response.isDeleted;
                        this.deadline = response.deadline;
                        console.log(this.startDate);
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
        console.log(this.startDate);
    }

    convertToDDMMYYYY(date: string): string {
        const [year, month, day] = date.split('-');
        return `${day}.${month}.${year}`;
    }
}
