import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { airlineguardGuard } from './airlineguard.guard';

describe('airlineguardGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => airlineguardGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
