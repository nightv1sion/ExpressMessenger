using ExpressMessenger.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");
        
        builder.HasKey(x => new
        {
            x.UserId,
            x.ChatId,
        });
        
        builder
            .Property(x => x.UserId)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.ChatId)
            .IsRequired()
            .ValueGeneratedNever();
    }
}