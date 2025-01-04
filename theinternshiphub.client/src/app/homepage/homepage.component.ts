import { Component } from '@angular/core';
import { InternshipService } from '../services/internship/internship.service';
import { IInternship } from '../dtos/IInternship';
import { Router } from '@angular/router';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrl: './homepage.component.css',
    standalone: true,
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
}
