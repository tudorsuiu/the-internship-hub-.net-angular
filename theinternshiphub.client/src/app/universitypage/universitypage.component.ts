import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
    selector: 'app-universitypage',
    templateUrl: './universitypage.component.html',
    styleUrl: './universitypage.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class UniversitypageComponent {
    constructor(private router: Router) {}

    logout() {
        this.router.navigate(['/login']);
        localStorage.clear();
    }
}
