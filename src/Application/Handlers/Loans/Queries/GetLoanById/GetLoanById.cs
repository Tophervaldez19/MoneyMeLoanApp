using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;

namespace MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanById;
public class GetLoanByIdQuery : IRequest<Result<LoanDto>>
{
    public Guid Id { get; set; }
}

public class GetLoanByIdQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetLoanByIdQuery, Result<LoanDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<LoanDto>> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        var dto = _mapper.Map<LoanDto>(loan);

        return Result<LoanDto>.Success(dto);
    }
}
