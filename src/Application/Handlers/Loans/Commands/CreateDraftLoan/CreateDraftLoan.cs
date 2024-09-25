using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Handlers.Loans.Commands.CreateDraftLoan;
public class CreateDraftLoanCommand : IRequest<Result<Guid>>
{
    public decimal AmountRequired { get; set; }
    public int Term { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string MobilePhone { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}

public class CreateDraftLoanCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateDraftLoanCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<Guid>> Handle(CreateDraftLoanCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.FirstName == request.FirstName && x.LastName == request.LastName && x.DateOfBirth == request.DateOfBirth);

        if (customer == null)
        {
            customer = new Customer()
            {
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                MobilePhone = request.MobilePhone,
                EmailAddress = request.EmailAddress,
            };
        }

        var loan = await _context.Loans.Where(x => x.Customer.FirstName == request.FirstName && x.Customer.LastName == request.LastName && x.Customer.DateOfBirth == request.DateOfBirth).FirstOrDefaultAsync();
        if (loan != null)
        {
            loan.Term = request.Term;
            loan.Amount = request.AmountRequired;

            await _context.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(loan.Id);
        }

        loan = new Loan()
        {
            Customer = customer,
            Amount = request.AmountRequired,
            Term = request.Term,
            Status = LoanStatus.Pending,
        };

        await _context.Loans.AddAsync(loan);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(loan.Id);
    }
}
