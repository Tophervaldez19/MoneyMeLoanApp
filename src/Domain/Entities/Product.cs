using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Domain.Entities;
public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? InterestFee { get; set; }
    public int? MonthsInterestFree { get; set; }
    public int? MinimumMonthsTerm { get; set; }
    public IList<Loan> Loans { get; private set; } = new List<Loan>();
}
