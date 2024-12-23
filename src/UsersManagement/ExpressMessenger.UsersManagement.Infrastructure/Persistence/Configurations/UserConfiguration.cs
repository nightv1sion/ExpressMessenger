using ExpressMessenger.UsersManagement.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpressMessenger.UsersManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("users");
        
        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.RefreshTokenExpired)
            .ValueGeneratedNever()
            .IsRequired(false);
        
        builder
            .Property(x => x.Created)
            .ValueGeneratedNever()
            .IsRequired(true);
    }
}