import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable()
export class AuthService {
  constructor() {}

  isLoggedIn(): Observable<boolean> {
    return of(false);
  }
}