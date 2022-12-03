import { Injectable } from '@angular/core';
import { ISantaService } from './santa';

@Injectable({
  providedIn: 'root'
})
export class SantaMockService implements ISantaService {

  constructor() { }
}
