using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Handlers.Customers.Dtos;
using MoneyMeLoan.Application.Handlers.Products.Dtos;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Handlers.Loans.Dtos;
public class LoanDto
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerDto Customer { get; set; } = default!;
    public Guid? ProductId { get; set; }
    public ProductDto Product { get; set; } = default!;
    public LoanStatus Status { get; set; }
    public int Term { get; set; }
}
