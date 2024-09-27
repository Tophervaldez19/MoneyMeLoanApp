using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Common.Interfaces;
public interface IPMTCalculator
{
    public PMTResult Calculate(Loan loan);
}
