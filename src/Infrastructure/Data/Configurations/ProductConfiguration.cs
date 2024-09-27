using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Infrastructure.Data.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(x => x.InterestFee).HasPrecision(38, 4);
        builder.Property(x => x.EstablishmentFee).HasPrecision(38, 4);
    }
}
