import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { LoanDto, LoansClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-loan-edit',
  templateUrl: './loan-edit.component.html',
  styleUrls: ['./loan-edit.component.css']
})
export class LoanEditComponent implements OnInit {

  loanId: string = "";
  loanDto: LoanDto;
  loanForm: FormGroup;
  loanMinAmount: number = 2100;
  loanMaxAmount: number = 15000;
  constructor(
    private activatedRoute: ActivatedRoute,
    private loansClient: LoansClient,
    private fb: FormBuilder
  ) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.loanId = params['id'];
      console.log(this.loanId + " loan Id");
    });

    if (this.loanId != "") {
      this.loansClient.getLoanById(this.loanId).subscribe(
        result => {
          this.loanDto = result.data;
          this.populateFormValue();
          console.log(this.loanDto);
        },
        error => console.error(error)
      );
    }


  }

  onSubmit() {
    if (this.loanForm.valid) {
      console.log(this.loanForm.value);
    }
  }

  initializeForm() {
    this.loanForm = this.fb.group({
      amount: [2100, [Validators.required, Validators.min(this.loanMinAmount), Validators.max(this.loanMaxAmount)]],
      term: [1, [Validators.required, Validators.min(1), Validators.max(24)]],
      productId: [''],
      title: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      mobilePhone: ['', Validators.required],
      emailAddress: ['', [Validators.required, Validators.email]],
      customerId: ['']
    });
  }

  populateFormValue() {
    this.loanForm.patchValue({
      amount: this.loanDto.amount,
      term: this.loanDto.term,
      title: this.loanDto.customer.title,
      firstName: this.loanDto.customer.firstName,
      lastName: this.loanDto.customer.lastName,
      dateOfBirth: this.loanDto.customer.dateOfBirth.toISOString().substring(0, 10),
      mobilePhone: this.loanDto.customer.mobilePhone,
      emailAddress: this.loanDto.customer.emailAddress,
      customerId: this.loanDto.customerId
    });

    Object.keys(this.loanForm.controls).forEach(field => {
      const control = this.loanForm.get(field);
      control?.markAsTouched({ onlySelf: true });
      control?.updateValueAndValidity();
    });
  }

  // Helper function to get the form control
  get f() {
    return this.loanForm.controls;
  }
}
