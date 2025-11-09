using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureRaces : IEntityTypeConfiguration<Race>
{
    public void Configure(EntityTypeBuilder<Race> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(r => r.Description)
               .HasMaxLength(2000);

        builder.Property(r => r.Source)
               .HasMaxLength(100);

        builder.Property(r => r.Size)
               .IsRequired();

        builder.Property(r => r.BaseSpeed)
               .IsRequired();

        builder.Property(r => r.Languages)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
               );

        builder.Property(r => r.AbilityScoreIncreases)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions) ?? new Dictionary<AbilityScore, int>()
               );

        builder.Property(r => r.Senses)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<Dictionary<SenseType, int>>(v, jsonOptions) ?? new Dictionary<SenseType, int>()
               );

        builder.HasMany(r => r.RacialFeatures)
               .WithMany(f => f.Races);

        // Owned Collection: SubRaces
        builder.OwnsMany(r => r.SubRaces, subrace =>
        {
            subrace.Property(sr => sr.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            subrace.Property(sr => sr.Description)
                   .HasMaxLength(2000);

            subrace.Property(sr => sr.Source)
                   .HasMaxLength(100);

            subrace.Property(sr => sr.SpeedOverride);

            subrace.Property(sr => sr.AbilityScoreIncreases)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions) ?? new Dictionary<AbilityScore, int>()
                   );

            subrace.Property(sr => sr.BonusLanguages)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
                   );

            subrace.Property(sr => sr.BonusWeaponProficiencies)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
                   );

            subrace.Property(sr => sr.BonusToolProficiencies)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
                   );

            subrace.Property(sr => sr.BonusSenses)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<Dictionary<SenseType, int>>(v, jsonOptions) ?? new Dictionary<SenseType, int>()
                   );

            subrace.Property(sr => sr.SubracialFeatures)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v.Select(f => f.Id).ToList(), jsonOptions),
                       v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions).Select(id => new Feature { Id = id }).ToList() ?? new List<Feature>()
                   );
        });
    }
}