import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LoanDto, LoansClient } from 'src/app/web-api-client';

@Component({
  selector: 'app-loan-confirm',
  templateUrl: './loan-confirm.component.html',
  styleUrls: ['./loan-confirm.component.css']
})
export class LoanConfirmComponent implements OnInit {

  loanId: string = "";
  loanDto: LoanDto;

  constructor(
    private activatedRoute: ActivatedRoute,
    private loansClient: LoansClient
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.loanId = params['id'];
      console.log(this.loanId + " loan Id");
    });

    if (this.loanId != "") {
      this.loansClient.getLoanById(this.loanId).subscribe(
        result => {
          this.loanDto = result.data;
          console.log(this.loanDto);
        },
        error => console.error(error)
      );
    }
  }
}
