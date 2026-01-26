using Domain.Models.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureMessageReadStatuses : IEntityTypeConfiguration<MessageReadStatus>
{
    public void Configure(EntityTypeBuilder<MessageReadStatus> builder)
    {
        builder.HasKey(mrs => new { mrs.MessageId, mrs.UserId });

        builder.Property(mrs => mrs.ReadAt)
            .IsRequired();

        builder.HasIndex(mrs => mrs.MessageId);
        builder.HasIndex(mrs => mrs.UserId);

        builder.HasOne(mrs => mrs.Message)
            .WithMany(m => m.ReadStatuses)
            .HasForeignKey(mrs => mrs.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mrs => mrs.User)
            .WithMany()
            .HasForeignKey(mrs => mrs.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}