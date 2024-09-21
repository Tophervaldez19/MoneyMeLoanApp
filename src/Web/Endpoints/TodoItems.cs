using MoneyMeLoan.Application.Common.Models;
using MoneyMeLoan.Application.TodoItems.Commands.CreateTodoItem;
using MoneyMeLoan.Application.TodoItems.Commands.DeleteTodoItem;
using MoneyMeLoan.Application.TodoItems.Commands.UpdateTodoItem;
using MoneyMeLoan.Application.TodoItems.Commands.UpdateTodoItemDetail;
using MoneyMeLoan.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace MoneyMeLoan.Web.Endpoints;

public class TodoItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTodoItemsWithPagination)
            .MapPost(CreateTodoItem)
            .MapPut(UpdateTodoItem, "{id}")
            .MapPut(UpdateTodoItemDetail, "UpdateDetail/{id}")
            .MapDelete(DeleteTodoItem, "{id}");
    }

    public Task<PaginatedList<TodoItemBriefDto>> GetTodoItemsWithPagination(ISender sender, [AsParameters] GetTodoItemsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<Guid> CreateTodoItem(ISender sender, CreateTodoItemCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateTodoItem(ISender sender, Guid id, UpdateTodoItemCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateTodoItemDetail(ISender sender, Guid id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTodoItem(ISender sender, Guid id)
    {
        await sender.Send(new DeleteTodoItemCommand(id));
        return Results.NoContent();
    }
}
