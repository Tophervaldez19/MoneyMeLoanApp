using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;

namespace MoneyMeLoan.Application.Handlers.Loans.Commands.ConfirmLoan;
public class ConfirmLoanValidator : AbstractValidator<ConfirmLoanCommand>
{
    private readonly IApplicationDbContext _context;
    public ConfirmLoanValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exist.");

        //RuleFor(x => x.Id)
        //    .MustAsync(CustomerIsAdult)
        //    .WithMessage("Customer must be at least 18 years old.");

        //RuleFor(x => x.Id)
        //    .MustAsync(CustomerMobileNotBlacklisted)
        //    .WithMessage("Customer's mobile number is blacklisted.");

        //RuleFor(x => x.Id)
        //    .MustAsync(CustomerEmailDomainNotBlacklisted)
        //    .WithMessage("Customer's email domain is blacklisted.");
    }

    private async Task<bool> IdExist(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Loans.AnyAsync(x => x.Id == id, cancellationToken);
    }

    //private async Task<bool> CustomerIsAdult(Guid id, CancellationToken cancellationToken)
    //{
    //    var loan = await _context.Loans
    //    .Include(x => x.Customer)
    //    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    //    if (loan == null || loan.Customer == null)
    //    {
    //        return false;
    //    }

    //    var today = DateOnly.FromDateTime(DateTime.Today);
    //    var birthday = loan.Customer.DateOfBirth;

    //    var age = today.Year - birthday.Year;

    //    if (birthday > today.AddYears(-age))
    //    {
    //        age--;
    //    }

    //    return age >= 18;
    //}

    //private async Task<bool> CustomerMobileNotBlacklisted(Guid id, CancellationToken cancellationToken)
    //{
    //    var loan = await _context.Loans
    //        .Include(x => x.Customer)
    //        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    //    if (loan == null || loan.Customer == null)
    //    {
    //        return false;
    //    }

    //    return !await _context.BlacklistedMobiles.AnyAsync(x => x.MobileNo == loan.Customer.MobilePhone, cancellationToken);
    //}

    //private async Task<bool> CustomerEmailDomainNotBlacklisted(Guid id, CancellationToken cancellationToken)
    //{
    //    var loan = await _context.Loans
    //        .Include(x => x.Customer)
    //        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    //    if (loan == null || loan.Customer == null || string.IsNullOrEmpty(loan.Customer.EmailAddress))
    //    {
    //        return false;
    //    }

    //    var emailDomain = loan.Customer.EmailAddress.Split('@').Last();
    //    return !await _context.BlacklistedDomains.AnyAsync(x => x.Name == emailDomain, cancellationToken);
    //}
}
