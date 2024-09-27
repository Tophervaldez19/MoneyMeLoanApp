using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;

namespace MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanConfirmation;
public class GetLoanConfirmationQuery : IRequest<Result<ConfirmLoanDto>>
{
    public Guid Id { get; set; }
}

public class GetLoanConfirmationQueryHandler(IApplicationDbContext context, IMapper mapper, IPMTCalculator pmtCalculator) : IRequestHandler<GetLoanConfirmationQuery, Result<ConfirmLoanDto>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IPMTCalculator _pmtCalculator = pmtCalculator;
    public async Task<Result<ConfirmLoanDto>> Handle(GetLoanConfirmationQuery request, CancellationToken cancellationToken)
    {
        var loan = await _context.Loans
            .AsNoTracking()
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        var pmtResult = _pmtCalculator.Calculate(loan!);

        var dto = _mapper.Map<ConfirmLoanDto>(loan);

        dto.MonthlyPayment = pmtResult.MonthlyPayment;
        dto.InterestFreePayment = pmtResult.MonthlyPaymentInterestFree;
        dto.TotalInterest = pmtResult.TotalInterest;

        return Result<ConfirmLoanDto>.Success(dto);
    }
}
