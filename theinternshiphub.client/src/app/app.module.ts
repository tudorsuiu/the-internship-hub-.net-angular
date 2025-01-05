import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CompanypageComponent } from './companypage/companypage.component';
import { UniversitypageComponent } from './universitypage/universitypage.component';
import { AuthInterceptor } from './interceptors/auth/auth.interceptor';
import { RequestInterceptor } from './interceptors/request/request.interceptor';
import { ApplicationsComponent } from './applications/applications.component';
import { AddInternshipComponent } from './add-internship/add-internship.component';

@NgModule({
    declarations: [
        AppComponent,
        CompanypageComponent,
        UniversitypageComponent,
        ApplicationsComponent,
    ],
    imports: [BrowserModule, HttpClientModule, AppRoutingModule],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: RequestInterceptor,
            multi: true,
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent],
})
export class AppModule {}
