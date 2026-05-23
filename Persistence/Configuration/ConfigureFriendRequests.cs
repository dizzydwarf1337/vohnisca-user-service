using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureFriendRequests : IEntityTypeConfiguration<FriendRequest>
{
    public void Configure(EntityTypeBuilder<FriendRequest> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.SentAt)
            .IsRequired();

        builder.HasIndex(x => x.SentBy);
        builder.HasIndex(x => x.SentTo);

        builder.HasOne(x => x.SentByUser)
            .WithMany(x => x.SentFriendRequests)
            .HasForeignKey(x => x.SentBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.SentToUser)
            .WithMany(x => x.ReceivedFriendRequests)
            .HasForeignKey(x => x.SentTo)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(x => new { x.SentBy, x.SentTo })
            .IsUnique()
            .HasFilter("\"Status\" = 0");
    }
}