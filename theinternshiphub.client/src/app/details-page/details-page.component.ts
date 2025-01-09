import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Role } from '../dtos/IUserRegister';
import { FileUploadService } from '../services/file-upload/file-upload.service';
import { IApplication } from '../dtos/IApplication';
import { IUser } from '../dtos/IUser';
import { UserService } from '../services/user/user.service';
import { ApplicationService } from '../services/application/application.service';

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

    user: IUser = {
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
    };

    selectedFile: File | null = null;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private internshipService: InternshipService,
        private location: Location,
        private fileUploadService: FileUploadService,
        private userService: UserService,
        private applicationService: ApplicationService
    ) {}

    ngOnInit(): void {
        this.internshipId = this.route.snapshot.paramMap.get('id');
        if (this.internshipId) {
            this.internshipService
                .getInternshipById(this.internshipId)
                .subscribe(
                    (response: IInternship) => {
                        this.internship = response;
                    },
                    (error: any) => {
                        console.log(error.message);
                    }
                );
        }

        this.userService.getLoggedUSer().subscribe(
            (response: IUser) => {
                this.user.id = response.id;
                this.user.firstName = response.firstName;
                this.user.lastName = response.lastName;
                this.user.email = response.email;
                this.user.phoneNumber = response.phoneNumber;
                this.user.company = response.company;
                this.user.role = response.role;
                this.user.city = response.city;
                this.user.isDeleted = response.isDeleted;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    onFileSelected(event: any): void {
        this.selectedFile = event.target.files[0];
    }

    onUpload(): void {
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
                    var application: IApplication = {
                        id: '00000000-0000-0000-0000-000000000000',
                        internshipId: this.internship.id,
                        internship: this.internship,
                        studentId: this.user.id,
                        student: this.user,
                        appliedDate: new Date(),
                        status: 'Applied',
                        cvFilePath: url,
                        isDeleted: false,
                        universityDocsFilePath: '',
                    };

                    this.applicationService
                        .addApplication(application)
                        .subscribe(
                            (response: any) => {
                                alert('Applied succesfully');
                                this.router.navigate(['/homepage']);
                            },
                            (error: any) => {
                                console.log(error.message);
                            }
                        );
                },
                error: err => {
                    console.error('Error uploading file:', err);
                    alert('Failed to upload your CV. Please try again.');
                },
            });
        } else {
            console.error('No file selected!');
            alert('Please upload your CV before applying.');
        }
    }

    navigateBack() {
        this.location.back();
    }
}
