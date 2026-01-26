using Domain.Models.Chats;
using Domain.Models.Notifications;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Database;

public class VohniscaDbContext : DbContext
{
    public VohniscaDbContext(DbContextOptions<VohniscaDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageAttachment> MessageAttachments { get; set; }
    public DbSet<MessageReadStatus> MessageReadStatuses { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ConfigureUsers());
        modelBuilder.ApplyConfiguration(new ConfigureChats());
        modelBuilder.ApplyConfiguration(new ConfigureMessages());
        modelBuilder.ApplyConfiguration(new ConfigureMessageAttachments());
        modelBuilder.ApplyConfiguration(new ConfigureMessageReadStatuses());
        modelBuilder.ApplyConfiguration(new ConfigureNotifications());
    }
}