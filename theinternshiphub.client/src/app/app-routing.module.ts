import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomepageComponent } from './homepage/homepage.component';
import { CompanypageComponent } from './companypage/companypage.component';
import { UniversitypageComponent } from './universitypage/universitypage.component';
import { AddInternshipComponent } from './add-internship/add-internship.component';

const routes: Routes = [
    { path: '', component: HomepageComponent, pathMatch: 'full' },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'homepage', component: HomepageComponent },
    { path: 'company', component: CompanypageComponent },
    { path: 'university', component: UniversitypageComponent },
    { path: 'addInternship', component: AddInternshipComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
