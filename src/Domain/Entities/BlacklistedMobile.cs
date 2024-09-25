using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Domain.Entities;
public class BlacklistedMobile : BaseAuditableEntity
{
    public string MobileNo { get; set; } = string.Empty;
}
