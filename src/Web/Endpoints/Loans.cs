
using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.Handlers.Loans.Commands.ConfirmLoan;
using MoneyMeLoan.Application.Handlers.Loans.Commands.CreateDraftLoan;
using MoneyMeLoan.Application.Handlers.Loans.Commands.UpdateLoan;
using MoneyMeLoan.Application.Handlers.Loans.Dtos;
using MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanById;
using MoneyMeLoan.Application.Handlers.Loans.Queries.GetLoanConfirmation;

namespace MoneyMeLoan.Web.Endpoints;

public class Loans : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateDraftLoan, nameof(CreateDraftLoan))
            .MapGet(GetLoanById, "GetLoanById/{id}")
            .MapGet(GetLoanConfirmById, "GetLoanConfirmById/{id}")
            .MapPut(UpdateLoan, "UpdateLoan")
            .MapPost(ConfirmLoan, "ConfirmLoan");
    }

    public async Task<Result<string>> CreateDraftLoan(ISender sender, CreateDraftLoanCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result<LoanDto>> GetLoanById(ISender sender, Guid id)
    {
        return await sender.Send(new GetLoanByIdQuery { Id = id });
    }

    public async Task<Result<ConfirmLoanDto>> GetLoanConfirmById(ISender sender, Guid id)
    {
        return await sender.Send(new GetLoanConfirmationQuery { Id = id });
    }

    public async Task<Result<Guid>> UpdateLoan(ISender sender, UpdateLoanCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> ConfirmLoan(ISender sender, Guid id)
    {
        return await sender.Send(new ConfirmLoanCommand { Id = id });
    }
}
