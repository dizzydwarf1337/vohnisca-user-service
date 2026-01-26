using Domain.Models.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureMessageAttachments : IEntityTypeConfiguration<MessageAttachment>
{
    public void Configure(EntityTypeBuilder<MessageAttachment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.FileUrl)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(a => a.Type)
            .IsRequired();

        builder.Property(a => a.FileSize)
            .IsRequired();

        builder.Property(a => a.UploadedAt)
            .IsRequired();

        builder.HasIndex(a => a.MessageId);
        builder.HasIndex(a => a.Type);

        builder.HasOne(a => a.Message)
            .WithMany(m => m.Attachments)
            .HasForeignKey(a => a.MessageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}