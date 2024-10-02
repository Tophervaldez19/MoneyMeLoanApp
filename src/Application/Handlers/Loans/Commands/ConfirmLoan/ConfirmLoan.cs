using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Handlers.Loans.Commands.ConfirmLoan;
public class ConfirmLoanCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

public class ConfirmLoanCommandHandler(IApplicationDbContext context) : IRequestHandler<ConfirmLoanCommand, Result>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result> Handle(ConfirmLoanCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<string>();
        var loan = await _context.Loans
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (!CustomerIsAdult(loan!.Customer))
        {
            errors.Add("Customer must be at least 18 years old.");
        }

        if(await CustomerMobileBlacklisted(loan.Customer,cancellationToken))
        {
            errors.Add("Customer's mobile number is blacklisted.");
        }

        if (await CustomerEmailDomainBlacklisted(loan.Customer, cancellationToken))
        {
            errors.Add("Customer's email domain is blacklisted.");
        }

        if (errors.Any())
        {
            return Result.Failure(errors);
        }

        loan!.Status = LoanStatus.Processing;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private bool CustomerIsAdult(Customer customer)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var birthday = customer.DateOfBirth;

        var age = today.Year - birthday.Year;

        if (birthday > today.AddYears(-age))
        {
            age--;
        }

        return age >= 18;
    }

    private async Task<bool> CustomerMobileBlacklisted(Customer customer, CancellationToken cancellationToken)
    {
        return await _context.BlacklistedMobiles.AnyAsync(x => x.MobileNo == customer.MobilePhone, cancellationToken);
    }

    private async Task<bool> CustomerEmailDomainBlacklisted(Customer customer, CancellationToken cancellationToken)
    {
        var emailDomain = customer.EmailAddress.Split('@').Last();
        return await _context.BlacklistedDomains.AnyAsync(x => x.Name == emailDomain, cancellationToken);
    }
}
