using MoneyMeLoan.Domain.Entities;

namespace MoneyMeLoan.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Loan> Loans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
