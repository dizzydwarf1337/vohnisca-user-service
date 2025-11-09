using Domain.Models.Characters;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database;

public class VohniscaDbContext : DbContext
{
    public VohniscaDbContext(DbContextOptions<VohniscaDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<Spell> Spells { get; set; }
    
}