using ExpressMessenger.UsersManagement.Application;
using Microsoft.EntityFrameworkCore;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence;

internal sealed class UnitOfWork : ApplicationDbContext, IUnitOfWork
{
    public UnitOfWork(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}