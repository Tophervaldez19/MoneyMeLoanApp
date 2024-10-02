using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Application.Common.Models;
public class PMTResult
{
    public decimal MonthlyPayment { get; set; }
    public decimal MonthlyPaymentInterestFree { get; set; }
    public decimal TotalInterest { get; set; }
    public decimal TotalRepayment { get; set; }

}
