using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Products.Dtos;

namespace MoneyMeLoan.Application.Handlers.Products.Queries.GetProducts;
public class GetProductsQuery : IRequest<Result<List<ProductDto>>>
{
}

public class GetProductsQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetProductsQuery, Result<List<ProductDto>>>
{
    private readonly IApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _context.Products.ToListAsync(cancellationToken);

        var dto = _mapper.Map<List<ProductDto>>(products);

        return Result<List<ProductDto>>.Success(dto);
    }
}
