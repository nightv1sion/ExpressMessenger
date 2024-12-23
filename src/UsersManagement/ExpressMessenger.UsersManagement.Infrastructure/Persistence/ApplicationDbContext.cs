using System.Reflection;
using ExpressMessenger.UsersManagement.Application;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        base.OnModelCreating(modelBuilder);
    }
}