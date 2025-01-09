import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IUser } from '../dtos/IUser';
import { UserService } from '../services/user/user.service';
import { IUniversityStudent } from '../dtos/IUniversityStudent';
import { IInternship } from '../dtos/IInternship';
import { InternshipService } from '../services/internship/internship.service';
import {
    debounceTime,
    distinctUntilChanged,
    forkJoin,
    Observable,
    Subject,
    switchMap,
} from 'rxjs';

@Component({
    selector: 'app-universitypage',
    templateUrl: './universitypage.component.html',
    styleUrl: './universitypage.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class UniversitypageComponent {
    students: IUniversityStudent[] = [];

    filteredStudents: IUniversityStudent[] = [];
    private searchSubject = new Subject<string>();

    constructor(
        private router: Router,
        private userService: UserService,
        private internshipService: InternshipService
    ) {}

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }

    ngOnInit() {
        this.userService.getStudents().subscribe(
            (response: IUniversityStudent[]) => {
                this.students = response;

                this.students.forEach(student =>
                    this.internshipService
                        .getInternshipById(student.internshipId)
                        .subscribe(
                            (response: IInternship) => {
                                student.internship = response;
                            },
                            error => {
                                console.log(error.message);
                                student.internship = undefined;
                            }
                        )
                );
                this.filteredStudents = [...this.students];
            },
            error => {
                console.error('Error fetching students:', error);
            }
        );

        this.searchSubject
            .pipe(
                debounceTime(250),
                distinctUntilChanged(),
                switchMap((searchTerm: string) =>
                    this.filterStudents(searchTerm)
                )
            )
            .subscribe(filtered => {
                this.filteredStudents = filtered;
            });
    }

    searchStudents(searchTerm: string) {
        this.searchSubject.next(searchTerm);
    }

    onInputChange(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input) {
            this.searchStudents(input.value);
        }
    }

    filterStudents(searchTerm: string) {
        return new Observable<IUniversityStudent[]>(observer => {
            const lowerCaseSearchTerm = searchTerm.toLowerCase();

            const filtered = this.students.filter(student => {
                return Object.values(student).some(value => {
                    if (typeof value === 'string') {
                        return value
                            .toLowerCase()
                            .includes(lowerCaseSearchTerm);
                    }
                    if (typeof value === 'number') {
                        return value.toString().includes(lowerCaseSearchTerm);
                    }
                    return false;
                });
            });

            observer.next(filtered);
            observer.complete();
        });
    }

    onSortChange(event: Event) {
        const selectElement = event.target as HTMLSelectElement;
        if (selectElement) {
            this.sortStudents(selectElement.value);
        }
    }

    sortStudents(sortOption: string) {
        if (!sortOption) {
            this.filteredStudents = [...this.students];
            return;
        }

        const [key, order] = sortOption.split('_');

        if (key === 'lastName') {
            this.filteredStudents = [...this.filteredStudents].sort((a, b) => {
                const nameA = a.studentLastName.toLowerCase();
                const nameB = b.studentLastName.toLowerCase();

                if (order === 'asc') {
                    return nameA < nameB ? -1 : nameA > nameB ? 1 : 0;
                } else {
                    return nameA > nameB ? -1 : nameA < nameB ? 1 : 0;
                }
            });
        }
    }
}
