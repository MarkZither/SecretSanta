import { Injectable } from '@angular/core';
import { ConcreteSantaService, ISantaService } from './santa';

@Injectable({
  providedIn: 'root'
})
export class SantaMockService extends ConcreteSantaService implements ISantaService {
  something: string | undefined;
  generate() {
      this.something = 'sdfsdf';
  }
}
