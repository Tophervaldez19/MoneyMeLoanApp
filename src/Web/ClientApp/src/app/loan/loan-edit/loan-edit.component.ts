import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoanDto, LoansClient, ProductDto, ProductsClient, UpdateLoanCommand } from 'src/app/web-api-client';

@Component({
  selector: 'app-loan-edit',
  templateUrl: './loan-edit.component.html',
  styleUrls: ['./loan-edit.component.css']
})
export class LoanEditComponent implements OnInit {

  loanId: string = "";
  loanDto: LoanDto;
  productDtos: ProductDto[];
  loanForm: FormGroup;
  loanMinAmount: number = 2100;
  loanMaxAmount: number = 15000;
  constructor(
    private activatedRoute: ActivatedRoute,
    private loansClient: LoansClient,
    private productClient: ProductsClient,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.initializeForm();
  }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.loanId = params['id'];
    });

    this.populateProducts();

    this.loanForm.get('productId')?.valueChanges.subscribe((selectedProductId: string) => {
      const termControl = this.loanForm.get('term');

      if (this.loanForm.get('productId').value) {
        let product = this.productDtos.find(x => x.id == this.loanForm.get('productId').value);

        if (product.minimumMonthsTerm > 0) {
          termControl.setValidators([Validators.min(product.minimumMonthsTerm)]);
        } else {
          termControl.setValidators([Validators.min(1)]);
        }
        termControl?.updateValueAndValidity();
      }

    });

    if (this.loanId != "") {
      this.loansClient.getLoanById(this.loanId).subscribe(
        result => {
          this.loanDto = result.data;
          this.populateFormValue();
        },
        error => console.error(error)
      );
    }

  }

  populateProducts() {
    this.productClient.getProducts().subscribe(result => {
      this.productDtos = result.data;
    },
      error => console.error(error)
    );
  }

  onSubmit() {
    if (this.loanForm.valid) {
      const updateLoanCommand = {
        loanId: this.f.loanId.value,
        amount: this.f.amount.value,
        term: this.f.term.value,
        productId: this.f.productId.value,
        customerId: this.f.customerId.value,
        title: this.f.title.value,
        firstName: this.f.firstName.value,
        lastName: this.f.lastName.value,
        dateOfBirth: this.f.dateOfBirth.value,
        mobilePhone: this.f.mobilePhone.value,
        emailAddress: this.f.emailAddress.value
      } as UpdateLoanCommand
      console.log(updateLoanCommand);

      this.loansClient.updateLoan(updateLoanCommand).subscribe(result => {
        if (result.succeeded) {
          this.router.navigate(['loan/confirm'], { queryParams: { id: result.data } });
        }
      }, error => console.error(error));
    }


  }

  initializeForm() {
    this.loanForm = this.fb.group({
      loanId: [''],
      amount: [2100, [Validators.required, Validators.min(this.loanMinAmount), Validators.max(this.loanMaxAmount)]],
      term: [1, [Validators.required, Validators.min(1), Validators.max(24)]],
      productId: ['', Validators.required],
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
      loanId: this.loanDto.id,
      amount: this.loanDto.amount,
      term: this.loanDto.term,
      title: this.loanDto.customer.title,
      firstName: this.loanDto.customer.firstName,
      lastName: this.loanDto.customer.lastName,
      dateOfBirth: this.loanDto.customer.dateOfBirth.toISOString().substring(0, 10),
      mobilePhone: this.loanDto.customer.mobilePhone,
      emailAddress: this.loanDto.customer.emailAddress,
      customerId: this.loanDto.customerId,
      productId: this.loanDto.productId
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
