using ExpressMessenger.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("messages");
        
        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.Text)
            .ValueGeneratedNever()
            .IsRequired(true);
        
        builder
            .Property(x => x.ChatId)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.SenderId)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.Sent)
            .IsRequired()
            .ValueGeneratedNever();
    }
}