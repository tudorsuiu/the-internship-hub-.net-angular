import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { IUser } from '../dtos/IUser';
import { UserService } from '../services/user/user.service';
import { IUniversityStudent } from '../dtos/IUniversityStudent';
import { IInternship } from '../dtos/IInternship';
import { InternshipService } from '../services/internship/internship.service';
import { forkJoin } from 'rxjs';

@Component({
    selector: 'app-universitypage',
    templateUrl: './universitypage.component.html',
    styleUrl: './universitypage.component.css',
    standalone: true,
    imports: [CommonModule, FormsModule],
})
export class UniversitypageComponent {
    students: IUniversityStudent[] = [];

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
            },
            error => {
                console.error('Error fetching students:', error);
            }
        );
    }
}
