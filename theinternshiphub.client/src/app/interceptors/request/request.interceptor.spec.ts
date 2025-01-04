import { TestBed } from '@angular/core/testing';
import { HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { RequestInterceptor } from './request.interceptor';
import { Observable } from 'rxjs';

describe('RequestInterceptor', () => {
    let interceptor: RequestInterceptor;

    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [RequestInterceptor],
        });
        interceptor = TestBed.inject(RequestInterceptor);
    });

    it('should be created', () => {
        expect(interceptor).toBeTruthy();
    });

    it('should add Authorization header when token is present', () => {
        const mockRequest = new HttpRequest('GET', '/api/test');
        const mockHandler: HttpHandler = {
            handle: (req: HttpRequest<any>): Observable<HttpEvent<any>> => {
                expect(req.headers.has('Authorization')).toBe(true);
                expect(req.headers.get('Authorization')).toBe(
                    'Bearer mock-token'
                );
                return new Observable<HttpEvent<any>>();
            },
        };

        spyOn(localStorage, 'getItem').and.returnValue('mock-token');
        interceptor.intercept(mockRequest, mockHandler).subscribe();
    });

    it('should not add Authorization header for Authentication endpoint', () => {
        const mockRequest = new HttpRequest('GET', '/api/Authentication');
        const mockHandler: HttpHandler = {
            handle: (req: HttpRequest<any>): Observable<HttpEvent<any>> => {
                expect(req.headers.has('Authorization')).toBe(false);
                return new Observable<HttpEvent<any>>();
            },
        };

        interceptor.intercept(mockRequest, mockHandler).subscribe();
    });
});
