import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpResponse, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';



@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
catchError(error => {
if (error.status === 401) {
    return throwError(error.statusText);
}
if (error instanceof HttpErrorResponse) {
    const Applicationerror = error.headers.get('Application-Error');
    if (Applicationerror) {
        return throwError(Applicationerror);
    }

    const servererror = error.error;

    let modalStateError = '';

    if ( servererror.errors && typeof servererror.errors === 'object') {
for (const key in servererror.errors) {
    if (servererror.errors[key]) {
        modalStateError += servererror.errors[key] + '\n';
    }
}
}
    return throwError(modalStateError || servererror || 'servererror');
}
})

    );
  }
}
export const ErrorInterceptorProvider = {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
   };