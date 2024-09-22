using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Handlers.Customers.Dtos;
public class CustomerDto
{
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string MobilePhone { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;

    public IList<LoanDto> Loans { get; private set; } = new List<LoanDto>();
}
