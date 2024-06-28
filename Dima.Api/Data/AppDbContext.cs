namespace Dima.Api.Data;

using System.Reflection;
using Dima.core.Models;
using Microsoft.EntityFrameworkCore;



public class AppDbContext : DbContext
{
    public DbSet<Category> Categories {get;set;} = null!;
    public DbSet<Transaction> Transactions {get;set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}