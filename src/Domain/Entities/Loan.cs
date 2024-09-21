using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Domain.Entities;
public class Loan : BaseAuditableEntity
{
    public decimal Amount { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public LoanStatus Status { get; set; }
    public int Term { get; set; }
}
