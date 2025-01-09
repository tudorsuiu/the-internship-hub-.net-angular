import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UniversitypageComponent } from './universitypage/universitypage.component';
import { AuthInterceptor } from './interceptors/auth/auth.interceptor';
import { RequestInterceptor } from './interceptors/request/request.interceptor';
import { UniversityApplicationsComponent } from './university-applications/university-applications.component';

@NgModule({
    declarations: [AppComponent, UniversityApplicationsComponent],
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
