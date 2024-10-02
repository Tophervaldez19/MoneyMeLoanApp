import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfirmLoanDto, LoanDto, LoansClient, LoanStatus } from 'src/app/web-api-client';

@Component({
  selector: 'app-loan-confirm',
  templateUrl: './loan-confirm.component.html',
  styleUrls: ['./loan-confirm.component.css']
})
export class LoanConfirmComponent implements OnInit {
  LoanStatus = LoanStatus;
  loanId: string = "";
  loanDto: ConfirmLoanDto;
  errors: string[] = [];
  constructor(
    private activatedRoute: ActivatedRoute,
    private loansClient: LoansClient
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.loanId = params['id'];
    });

    if (this.loanId != "") {
      this.loansClient.getLoanConfirmById(this.loanId).subscribe(
        result => {
          this.loanDto = result.data;
          this.loanDto.status
          console.log(this.loanDto);
        },
        error => console.error(error)
      );
    }
  }

  onSubmit(): void {
    this.loansClient.confirmLoan(this.loanId).subscribe(result => {
      if (result.succeeded) {
        window.location.reload();
      }

      if (result.errors) {
        this.errors = result.errors
      }
    }, error => console.error(error));
  }

  isSubmitButtonDisabled(): boolean {
    return this.loanDto.status == LoanStatus.Processing;
  }
}
