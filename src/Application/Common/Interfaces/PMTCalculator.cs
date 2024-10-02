using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Common.Interfaces;
public class PMTCalculator : IPMTCalculator
{
    public PMTResult Calculate(Loan loan)
    {
        decimal interestFreePayment = loan.Amount / loan.Term;

        // Calculate the remaining loan balance after the interest-free period
        decimal remainingPrincipal = loan.Amount - (interestFreePayment * loan.Product.MonthsInterestFree ?? 0);

        // Convert annual interest rate to monthly interest rate
        decimal monthlyInterestRate = ((loan.Product.InterestFee ?? 0) / 100) / 12;

        // Calculate the monthly payment for the remaining 22 months using the loan amortization formula
        int remainingMonths = loan.Term - (loan.Product.MonthsInterestFree ?? 0);
        double monthlyPaymentWithInterest;
        if (monthlyInterestRate == 0)
        {
            monthlyPaymentWithInterest = (double)(loan.Amount / loan.Term);
        }
        else
        {
            monthlyPaymentWithInterest = (double)remainingPrincipal * (double)monthlyInterestRate * Math.Pow(1 + (double)monthlyInterestRate, remainingMonths)
                              / (Math.Pow(1 + (double)monthlyInterestRate, remainingMonths) - 1);
        }

        decimal monthlyPaymentFromEstablishmentFee = loan.Product.EstablishmentFee / loan.Term;

        // Calculate total paid for the interest-free period and interest-bearing period
        decimal totalPaidInterestFree = interestFreePayment * (loan.Product.MonthsInterestFree ?? 0);
        decimal totalPaidWithInterest = (decimal)monthlyPaymentWithInterest * remainingMonths;

        // Total payment
        decimal totalPayments = totalPaidInterestFree + totalPaidWithInterest;

        // Total interest
        decimal totalInterest = totalPayments - loan.Amount;

        var result = new PMTResult()
        {
            MonthlyPayment = (decimal)monthlyPaymentWithInterest + monthlyPaymentFromEstablishmentFee,
            MonthlyPaymentInterestFree = interestFreePayment + monthlyPaymentFromEstablishmentFee,
            TotalInterest = totalInterest,
            TotalRepayment = totalPayments + loan.Product.EstablishmentFee
        };

        return result;
    }
}
