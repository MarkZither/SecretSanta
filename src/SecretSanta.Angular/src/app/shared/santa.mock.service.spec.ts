import { TestBed } from '@angular/core/testing';

import { SantaMockService } from './santa.mock.service';

describe('SantaMockService', () => {
  let service: SantaMockService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SantaMockService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
