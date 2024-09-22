import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanConfirmComponent } from './loan-confirm.component';

describe('LoanConfirmComponent', () => {
  let component: LoanConfirmComponent;
  let fixture: ComponentFixture<LoanConfirmComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoanConfirmComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoanConfirmComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
