import { TestBed } from '@angular/core/testing';

import { SantaService } from './santa.service';

describe('SantaService', () => {
  let service: SantaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SantaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
