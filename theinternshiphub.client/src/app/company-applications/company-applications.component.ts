import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ApplicationService } from '../services/application/application.service';
import { IApplication } from '../dtos/IApplication';
import {
    debounceTime,
    distinctUntilChanged,
    Observable,
    Subject,
    switchMap,
} from 'rxjs';
import { Router } from '@angular/router';
import { ICandidateProfileDTO } from '../dtos/CandidateProfile/ICandidateProfileDTO';

@Component({
    selector: 'app-company-applications',
    templateUrl: './company-applications.component.html',
    styleUrl: './company-applications.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class CompanyApplicationsComponent {
    applications: IApplication[] = [];
    filteredApplications: IApplication[] = [];
    private searchSubject = new Subject<string>();

    candidateProfile: ICandidateProfileDTO = {
        personalInformation: {
            name: '',
            location: '',
            contact: '',
        },
        education: [
            {
                institution: '',
                degree: '',
                graduated: '',
            },
        ],
        professionalExperience: [
            {
                company: '',
                role: '',
                duration: '',
                responsibilities: '',
            },
        ],
        technicalSkills: [''],
        certifications: [''],
        projects: [
            {
                projectName: '',
                description: '',
                technologiesUsed: [''],
            },
        ],
        languages: [
            {
                name: '',
                level: '',
            },
        ],
        additionalInformation: {
            interests: '',
            volunteer: '',
        },
    };

    constructor(
        private applicationService: ApplicationService,
        private router: Router
    ) {}

    ngOnInit() {
        this.loadApplications();

        this.searchSubject
            .pipe(
                debounceTime(250),
                distinctUntilChanged(),
                switchMap((searchTerm: string) =>
                    this.filterApplications(searchTerm)
                )
            )
            .subscribe(filtered => {
                this.filteredApplications = filtered;
            });
    }

    loadApplications() {
        this.applicationService.getApplications().subscribe(
            (response: any) => {
                this.applications = response;
                this.filteredApplications = [...this.applications];
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    updateStatus(application: IApplication) {
        const updatedApplication = {
            ...application,
            status: application.status,
        };

        this.applicationService.updateApplication(updatedApplication).subscribe(
            (response: any) => {},
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    searchApplications(searchTerm: string) {
        this.searchSubject.next(searchTerm);
    }

    onInputChange(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input) {
            this.searchApplications(input.value);
        }
    }

    filterApplications(searchTerm: string) {
        return new Observable<IApplication[]>(observer => {
            const lowerCaseSearchTerm = searchTerm.toLowerCase();

            const filtered = this.applications.filter(application => {
                return (
                    (application.internship.title &&
                        application.internship.title
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm)) ||
                    (application.student.firstName &&
                        application.student.firstName
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm)) ||
                    (application.student.lastName &&
                        application.student.lastName
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm)) ||
                    (application.student.company.cO_NAME &&
                        application.student.company.cO_NAME
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm)) ||
                    (application.status &&
                        application.status
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm)) ||
                    (application.appliedDate &&
                        application.appliedDate
                            .toString()
                            .includes(lowerCaseSearchTerm))
                );
            });

            observer.next(filtered);
            observer.complete();
        });
    }

    onSortChange(event: Event) {
        const selectElement = event.target as HTMLSelectElement;
        if (selectElement) {
            this.sortApplications(selectElement.value);
        }
    }

    sortApplications(sortOption: string) {
        if (!sortOption) {
            this.filteredApplications = [...this.applications];
            return;
        }

        const [key, order] = sortOption.split('_');

        if (key === 'appliedDate') {
            this.filteredApplications = [...this.applications].sort((a, b) => {
                const dateA = new Date(a.appliedDate).getTime();
                const dateB = new Date(b.appliedDate).getTime();
                if (order === 'asc') {
                    return dateA - dateB;
                } else {
                    return dateB - dateA;
                }
            });
        }
    }

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }

    generateCandidateProfile(applicationId: string) {
        this.applicationService.getCandidateProfile(applicationId).subscribe(
            (response: any) => {
                this.candidateProfile = response;
                console.log(this.candidateProfile);
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }
}
