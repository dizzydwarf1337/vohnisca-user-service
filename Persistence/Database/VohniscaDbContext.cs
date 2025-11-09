using Domain.Models.Characters;
using Domain.Models.Characters.Items;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.Configuration;

namespace Persistence.Database;

public class VohniscaDbContext : DbContext
{
    public VohniscaDbContext(DbContextOptions<VohniscaDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Background> Backgrounds { get; set; }
    public DbSet<Spell> Spells { get; set; }
    public DbSet<Item> Items { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new ConfigureUsers());
        modelBuilder.ApplyConfiguration(new ConfigureCharacter());
        modelBuilder.ApplyConfiguration(new ConfigureFeatures());
        modelBuilder.ApplyConfiguration(new ConfigureClasses());
        modelBuilder.ApplyConfiguration(new ConfigureRaces());
        modelBuilder.ApplyConfiguration(new ConfigureBackgrounds());
        modelBuilder.ApplyConfiguration(new ConfigureSpells());
        modelBuilder.ApplyConfiguration(new ConfigureItems());
        modelBuilder.ApplyConfiguration(new ConfigureWeapons());
        modelBuilder.ApplyConfiguration(new ConfigureArmors());
    }
}