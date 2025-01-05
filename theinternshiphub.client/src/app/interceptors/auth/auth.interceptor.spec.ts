import { TestBed } from '@angular/core/testing';
import {
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse,
} from '@angular/common/http';
import { AuthInterceptor } from './auth.interceptor';
import { Observable, throwError } from 'rxjs';

describe('AuthInterceptor', () => {
    let interceptor: AuthInterceptor;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [AuthInterceptor],
        });
        interceptor = TestBed.inject(AuthInterceptor);
    });

    it('should be created', () => {
        expect(interceptor).toBeTruthy();
    });

    it('should handle 401 error and redirect to login', () => {
        const mockRequest = new HttpRequest('GET', '/api/test');
        const mockHandler: HttpHandler = {
            handle: () => {
                return throwError(() => new HttpErrorResponse({ status: 401 }));
            },
        };

        spyOn(window, 'alert');

        interceptor.intercept(mockRequest, mockHandler).subscribe({
            error: error => {
                expect(error.status).toBe(401);
                expect(window.alert).toHaveBeenCalledWith(
                    'Session expired. Please log in again'
                );
                expect(window.location.href).toBe('/login');
            },
        });
    });

    it('should pass through other errors', () => {
        const mockRequest = new HttpRequest('GET', '/api/test');
        const mockHandler: HttpHandler = {
            handle: () => {
                return throwError(() => new HttpErrorResponse({ status: 500 }));
            },
        };

        interceptor.intercept(mockRequest, mockHandler).subscribe({
            error: error => {
                expect(error.status).toBe(500);
                expect(window.alert).not.toHaveBeenCalled();
                expect(window.location.href).not.toBe('/login');
            },
        });
    });
});
