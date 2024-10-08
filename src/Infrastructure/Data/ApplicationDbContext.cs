﻿using System.Reflection;
using MoneyMeLoan.Application.Common.Interfaces;
using MoneyMeLoan.Domain.Entities;
using MoneyMeLoan.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoneyMeLoan.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<BlacklistedDomain> BlacklistedDomains => Set<BlacklistedDomain>();
    public DbSet<BlacklistedMobile> BlacklistedMobiles => Set<BlacklistedMobile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
