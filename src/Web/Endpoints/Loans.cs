
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Loans.Commands.CreateDraftLoan;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;
using MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanById;

namespace MoneyMeLoan.Web.Endpoints;

public class Loans : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateDraftLoan, nameof(CreateDraftLoan))
            .MapGet(GetLoanById, "GetLoanById/{id}");
    }

    public async Task<Result<Guid>> CreateDraftLoan(ISender sender, CreateDraftLoanCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result<LoanDto>> GetLoanById(ISender sender, Guid id)
    {
        return await sender.Send(new GetLoanByIdQuery { Id = id });
    }
}
