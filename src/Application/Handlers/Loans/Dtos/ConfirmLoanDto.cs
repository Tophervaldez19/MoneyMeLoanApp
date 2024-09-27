using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Application.Handlers.Loans.Dtos;
public class ConfirmLoanDto : LoanDto
{
    public decimal MonthlyPayment { get; set; }
    public decimal InterestFreePayment { get; set; }
    public decimal TotalInterest { get; set; }
}
