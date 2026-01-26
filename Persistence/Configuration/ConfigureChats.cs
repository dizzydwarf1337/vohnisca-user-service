using Domain.Models.Chats;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureChats : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.Property(c => c.AvatarUrl)
            .HasMaxLength(1000);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasIndex(c => c.CreatedById);
        builder.HasIndex(c => c.CreatedAt);

        builder.HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.PinnedMessage)
            .WithMany()
            .HasForeignKey(c => c.PinnedMessageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(c => c.Participants)
            .WithMany(u => u.Chats)
            .UsingEntity<Dictionary<string, object>>(
                "ChatParticipants",
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Chat>().WithMany().HasForeignKey("ChatId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("ChatId", "UserId")
            );

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}