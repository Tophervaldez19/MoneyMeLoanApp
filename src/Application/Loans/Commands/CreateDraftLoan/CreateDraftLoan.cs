﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MoneyMeLoan.Application.Common.Interfaces;
//using MoneyMeLoan.Application.Common.Models;
//using MoneyMeLoan.Domain.Entities;

//namespace MoneyMeLoan.Application.Loans.Commands.CreateDraftLoan;
//public class CreateDraftLoanCommand : IRequest<Result<string>>
//{
//    public decimal AmountRequired { get; set; }
//    public int Termm { get; set; }
//    public string Title { get; set; } = string.Empty;
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; } = string.Empty;
//    public DateOnly DateOfBirth { get; set; }
//    public string MobilePhone { get; set; } = string.Empty;
//    public string EmailAddress { get; set; } = string.Empty;
//}

//public class CreateDraftLoanCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateDraftLoanCommand, Result<string>>
//{
//    private readonly IApplicationDbContext _context = context;
//    public async Task<Result<string>> Handle(CreateDraftLoanCommand request, CancellationToken cancellationToken)
//    {
//        var customer = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.DateOfBirth == request.DateOfBirth);

//        if (customer == null)
//        {
//            customer = new Customer()
//            {
//                Title = request.Title,
//                FirstName = request.FirstName,
//                LastName = request.LastName,
//                DateOfBirth = request.DateOfBirth,
//                MobilePhone = request.MobilePhone,
//                EmailAddress = request.EmailAddress,
//            };
//        }




//    }
//}
