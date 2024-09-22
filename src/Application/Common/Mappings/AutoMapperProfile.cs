using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Handlers.Customers.Dtos;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;
using MoneyMeLoan.Application.Handlers.Products.Dtos;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Common.Mappings;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //Customer
        CreateMap<Customer, CustomerDto>().ReverseMap();

        //Product
        CreateMap<Product, ProductDto>().ReverseMap();

        //Loan
        CreateMap<Loan,LoanDto>().ReverseMap();

    }
}
