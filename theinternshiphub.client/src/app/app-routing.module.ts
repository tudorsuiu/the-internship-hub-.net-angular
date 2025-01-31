import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomepageComponent } from './homepage/homepage.component';
import { CompanypageComponent } from './companypage/companypage.component';
import { UniversitypageComponent } from './universitypage/universitypage.component';
import { AddInternshipComponent } from './add-internship/add-internship.component';
import { DetailsPageComponent } from './details-page/details-page.component';
import { ApplicationsComponent } from './applications/applications.component';
import { MyaccountComponent } from './myaccount/myaccount.component';
import { UpdateInternshipComponent } from './update-internship/update-internship.component';
import { CompanyApplicationsComponent } from './company-applications/company-applications.component';

const routes: Routes = [
    { path: '', component: LoginComponent, pathMatch: 'full' },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'homepage', component: HomepageComponent },
    { path: 'company', component: CompanypageComponent },
    { path: 'university', component: UniversitypageComponent },
    { path: 'addInternship', component: AddInternshipComponent },
    { path: 'detailsPage/:id', component: DetailsPageComponent },
    { path: 'applications', component: ApplicationsComponent },
    { path: 'myaccount', component: MyaccountComponent },
    { path: 'updateInternship/:id', component: UpdateInternshipComponent },
    { path: 'companyApplications', component: CompanyApplicationsComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
