import { Component } from '@angular/core';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrl: './homepage.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class HomepageComponent {
    internships: IInternship[] = [];

    constructor(
        private router: Router,
        private internshipService: InternshipService
    ) {}

    ngOnInit() {
        this.loadInternships();
    }

    loadInternships() {
        this.internshipService.getInternships().subscribe(
            (response: IInternship[]) => {
                this.internships = response;
            },
            (error: any) => {
                console.log(error.message);
            }
        );
    }

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }

    navigateToDetailsPage(id: string) {
        this.router.navigate(['/detailsPage', id]);
    }
}
