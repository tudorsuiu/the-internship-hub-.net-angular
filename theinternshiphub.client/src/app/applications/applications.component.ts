import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ApplicationService } from '../services/application/application.service';
import { IApplication } from '../dtos/IApplication';
import { FileUploadService } from '../services/file-upload/file-upload.service';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-applications',
    templateUrl: './applications.component.html',
    styleUrl: './applications.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class ApplicationsComponent {
    applications: IApplication[] = [];

    selectedFile: File | null = null;
    private searchSubject = new Subject<string>();

    constructor(
        private router: Router,
        private applicationService: ApplicationService,
        private fileUploadService: FileUploadService
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

    onFileSelected(event: any, application: IApplication): void {
        this.selectedFile = event.target.files[0];

        if (this.selectedFile) {
            const fileName = this.selectedFile.name;
            const fileExtension = fileName.split('.').pop()?.toLowerCase();

            if (fileExtension !== 'pdf' && fileExtension !== 'docx') {
                console.error(
                    'Invalid file format! Please upload a PDF or DOCX.'
                );
                alert('Please upload a valid CV in PDF or DOCX format.');
                return;
            }

            this.fileUploadService.uploadFile(this.selectedFile).subscribe({
                next: response => {
                    var url = response.url;
                    var app: IApplication = {
                        ...application,
                        universityDocsFilePath: url,
                    };

                    console.log(app);

                    this.applicationService.updateApplication(app).subscribe(
                        (response: any) => {
                            alert('Documents uploaded succesfully');
                            this.router.navigate(['/applications']);
                        },
                        (error: any) => {
                            console.log(error.message);
                        }
                    );
                },
                error: err => {
                    console.error('Error uploading file:', err);
                    alert('Failed to upload your documents. Please try again.');
                },
            });
        } else {
            console.error('No file selected!');
        }
    }

    uploadDocumentsForUniversity() {}
}
