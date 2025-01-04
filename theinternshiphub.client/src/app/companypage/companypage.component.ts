import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-companypage',
    templateUrl: './companypage.component.html',
    styleUrl: './companypage.component.css',
})
export class CompanypageComponent {
    constructor(private router: Router) {}

    navigateToAddInternship() {
        this.router.navigate(['/addInternship']);
    }
}
