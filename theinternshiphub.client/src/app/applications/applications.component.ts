import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApplicationService } from '../services/application/application.service';
import { IApplication } from '../dtos/IApplication';

@Component({
    selector: 'app-applications',
    templateUrl: './applications.component.html',
    styleUrl: './applications.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class ApplicationsComponent {
    applications: IApplication[] = [];

    constructor(
        private router: Router,
        private applicationService: ApplicationService
    ) {}

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }

    ngOnInit() {
        this.loadApplications();
    }

    loadApplications() {
        this.applicationService.getApplications().subscribe(
            (response: any) => {
                this.applications = response;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    confirmAcceptance(application: IApplication) {
        const updatedApplication = { ...application, status: 'Confirmed' };

        this.applicationService.updateApplication(updatedApplication).subscribe(
            (response: any) => {
                this.loadApplications();
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    declineAcceptance(application: IApplication) {
        const updatedApplication = { ...application, status: 'Declined' };

        console.log(updatedApplication);

        this.applicationService.updateApplication(updatedApplication).subscribe(
            (response: any) => {
                this.loadApplications();
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    uploadDocumentsForUniversity() {}
}
