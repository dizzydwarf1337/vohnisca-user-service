using Domain.Models.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureSpells : IEntityTypeConfiguration<Spell>
{
    public void Configure(EntityTypeBuilder<Spell> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Source)
            .HasMaxLength(100);

        builder.Property(s => s.Level)
            .IsRequired();

        builder.Property(s => s.School)
            .IsRequired();

        builder.Property(s => s.CastingTime)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Range)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.HasVerbalComponent)
            .IsRequired();

        builder.Property(s => s.HasSomaticComponent)
            .IsRequired();

        builder.Property(s => s.HasMaterialComponent)
            .IsRequired();

        builder.Property(s => s.MaterialComponents)
            .HasMaxLength(500);

        builder.Property(s => s.Duration)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(s => s.AtHigherLevels)
            .HasMaxLength(2000);

        builder.Property(s => s.IsRitual)
            .IsRequired();

        builder.Property(s => s.IsConcentration)
            .IsRequired();

        builder.HasMany(s => s.AvailableForClasses)
            .WithMany(c => c.AvailableSpells);

        builder.HasIndex(s => s.Level);
        builder.HasIndex(s => s.School);
        builder.HasIndex(s => s.Name);
    }
}