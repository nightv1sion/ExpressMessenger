using ExpressMessenger.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpressMessenger.Chatting.Infrastructure.Persistence.Configurations;

internal sealed class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(x => x.Id);

        builder.ToTable("chats");
        
        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
        
        builder
            .Property(x => x.Type)
            .ValueGeneratedNever()
            .HasMaxLength(16)
            .HasConversion<string>()
            .IsRequired(true);

        builder
            .HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey(x => x.ChatId);

        builder
            .Navigation(x => x.Messages)
            .AutoInclude();
        
        builder
            .HasMany(x => x.Members)
            .WithOne()
            .HasForeignKey(x => x.ChatId);

        builder
            .Navigation(x => x.Members)
            .AutoInclude();
    }
}