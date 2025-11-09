using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureUsers : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Bio)
            .HasMaxLength(500);
        
        builder.OwnsOne(u => u.UserSettings, us =>
        {
            us.Property(s => s.Status).IsRequired();
            us.Property(s => s.ActivatedAt);
            us.Property(s => s.BlockedAt);
            us.Property(s => s.DeletedAt);
        });
        
        builder.HasMany(u => u.Friends)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "UserFriend",
                j => j.HasOne<User>().WithMany().HasForeignKey("FriendId").OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasKey("UserId", "FriendId")
            );
    } 
}
