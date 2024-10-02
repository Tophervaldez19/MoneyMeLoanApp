using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;

namespace MoneyMeLoan.Application.Handlers.Products.Dtos;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? InterestFee { get; set; }
    public int? MonthsInterestFree { get; set; }
    public int? MinimumMonthsTerm { get; set; }
    public decimal EstablishmentFee { get; set; }
    public IList<LoanDto> Loans { get; private set; } = new List<LoanDto>();
}
