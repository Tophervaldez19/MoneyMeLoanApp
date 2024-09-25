using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;

namespace MoneyMeLoan.Application.Handlers.Loans.Commands.UpdateLoan;
public class UpdateLoanCommand : IRequest<Result<Guid>>
{
    public Guid LoanId { get; set; }
    public decimal Amount { get; set; }
    public int term { get; set; }
    public Guid ProductId { get; set; }
    public Guid CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string MobilePhone { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}

public class UpdateLoanCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateLoanCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<Result<Guid>> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans.Include(x => x.Customer).FirstOrDefaultAsync(x => x.Id == request.LoanId);

        loan!.Amount = request.Amount;
        loan.Term = request.term;
        loan.ProductId = request.ProductId;
        loan.Customer.Title = request.Title;
        loan.Customer.FirstName = request.FirstName;
        loan.Customer.LastName = request.LastName;
        loan.Customer.DateOfBirth = request.DateOfBirth;
        loan.Customer.MobilePhone = request.MobilePhone;
        loan.Customer.EmailAddress = request.EmailAddress;
        
        _context.Loans.Update(loan);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(loan.Id);
    }
}
