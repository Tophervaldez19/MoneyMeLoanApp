using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Domain.Entities;
public class Customer : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string MobilePhone { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;

    public IList<Loan> Loans { get; private set; } = new List<Loan>();
}
