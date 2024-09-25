
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Products.Dtos;
using MoneyMeLoan.Application.Handlers.Products.Queries.GetProducts;

namespace MoneyMeLoan.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetProducts);
    }

    public async Task<Result<List<ProductDto>>> GetProducts(ISender sender)
    {
        return await sender.Send(new GetProductsQuery());
    }
}
