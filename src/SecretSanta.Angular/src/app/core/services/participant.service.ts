import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { EnvService } from '@app/shared/env.service';
import { LoggerService } from '@app/shared/logger.service';
import { Participant } from '@app/shared/participant';

@Injectable({
  providedIn: 'root'
})
export class ParticipantService {

  private baseUrl = new URL(this.env.apiUrl);
  private participantsUrlPath = 'api/participant';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private env: EnvService, private logger: LoggerService) { }

  /** GET heroes from the server */
  getParticipants(): Observable<Participant[]> {
    this.baseUrl.pathname = this.participantsUrlPath;
    const participantsUrl = this.baseUrl.href;
    this.logger.info('url is ${participantsUrl}');
    return this.http.get<Participant[]>(participantsUrl)
      .pipe(
        tap(_ => this.logger.info('fetched heroes')),
        catchError(this.handleError<Participant[]>('getHeroes', []))
      );
  }

    /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
    private handleError<T>(operation = 'operation', result?: T) {
      return (error: any): Observable<T> => {
  
        // TODO: send the error to remote logging infrastructure
        console.error(error); // log to console instead
  
        // TODO: better job of transforming error for user consumption
        this.logger.error(`${operation} failed: ${error.message}`);
  
        // Let the app keep running by returning an empty result.
        return of(result as T);
      };
    }
}
