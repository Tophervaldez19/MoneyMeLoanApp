
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Loans.Commands.CreateDraftLoan;

namespace MoneyMeLoan.Web.Endpoints;

public class Loans : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateDraftLoan, nameof(CreateDraftLoan));
    }

    public Task<Result<string>> CreateDraftLoan(ISender sender,CreateDraftLoanCommand command)
    {
        return sender.Send(command);
    }
}
