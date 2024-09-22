using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;

namespace MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanById;
public class GetLoanByIdValidator : AbstractValidator<GetLoanByIdQuery>
{
    private readonly IApplicationDbContext _context;
    public GetLoanByIdValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id).NotEmpty()
            .MustAsync(IdExist)
            .WithMessage((command, id) => $"The Id:{id} does not exists.");
    }

    private async Task<bool> IdExist(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Loans.AnyAsync(x => x.Id == id, cancellationToken);
    }
}
