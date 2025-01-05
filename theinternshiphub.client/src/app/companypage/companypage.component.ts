import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-companypage',
    templateUrl: './companypage.component.html',
    styleUrl: './companypage.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class CompanypageComponent {
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

    navigateToAddInternship() {
        this.router.navigate(['/addInternship']);
    }

    selectInternship(internship: IInternship) {}
}
