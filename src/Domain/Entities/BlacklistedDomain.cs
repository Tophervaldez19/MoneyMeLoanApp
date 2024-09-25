using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMeLoan.Domain.Entities;
public class BlacklistedDomain : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
}
