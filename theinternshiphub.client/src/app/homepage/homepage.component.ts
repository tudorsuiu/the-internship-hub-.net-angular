import { Component } from '@angular/core';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrls: ['./homepage.component.css'],
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class HomepageComponent {
    internships: IInternship[] = [];
    filteredInternships: IInternship[] = [];
    private searchSubject = new Subject<string>();

    constructor(
        private router: Router,
        private internshipService: InternshipService
    ) {}

    ngOnInit() {
        this.loadInternships();

        this.searchSubject
            .pipe(
                debounceTime(250),
                distinctUntilChanged(),
                switchMap((searchTerm: string) =>
                    this.filterInternships(searchTerm)
                )
            )
            .subscribe(filtered => {
                this.filteredInternships = filtered;
            });
    }

    loadInternships() {
        this.internshipService.getInternships().subscribe(
            (response: IInternship[]) => {
                this.internships = response;
                this.filteredInternships = response;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    searchInternships(searchTerm: string) {
        this.searchSubject.next(searchTerm);
    }

    onInputChange(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input) {
            this.searchInternships(input.value);
        }
    }

    filterInternships(searchTerm: string) {
        return new Observable<IInternship[]>(observer => {
            const lowerCaseSearchTerm = searchTerm.toLowerCase();

            const filtered = this.internships.filter(internship => {
                return Object.values(internship).some(value => {
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

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }

    navigateToDetailsPage(id: string) {
        this.router.navigate(['/detailsPage', id]);
    }

    onSortChange(event: Event) {
        const selectElement = event.target as HTMLSelectElement;
        if (selectElement) {
            this.sortInternships(selectElement.value);
        }
    }

    sortInternships(sortOption: string) {
        if (!sortOption) {
            this.filteredInternships = [...this.internships];
            return;
        }

        const [key, order] = sortOption.split('_');

        this.filteredInternships = [...this.filteredInternships].sort(
            (a, b) => {
                if (key === 'compensation') {
                    const compA = a.compensation || 0;
                    const compB = b.compensation || 0;

                    return order === 'asc' ? compA - compB : compB - compA;
                }
                return 0;
            }
        );
    }
}
