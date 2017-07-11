import { TestBed, inject } from '@angular/core/testing';

import { AdminScriptService } from './adminscript.service';

describe('AdminscriptService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AdminScriptService]
    });
  });

  it('should be created', inject([AdminScriptService], (service: AdminScriptService) => {
    expect(service).toBeTruthy();
  }));
});
