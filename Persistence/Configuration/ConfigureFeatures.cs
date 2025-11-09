using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureFeatures : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(f => f.Description)
               .HasMaxLength(2000);

        builder.Property(f => f.Source)
               .HasMaxLength(100);

        builder.Property(f => f.SourceType)
               .IsRequired();

        builder.Property(f => f.Type)
               .IsRequired();

        builder.Property(f => f.GrantsHalfASI);

        builder.Property(f => f.IsSelectableAsFeat);

        builder.Property(f => f.AllowedAbilitiesForHalfASI)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<AbilityScore>>(v, jsonOptions) ?? new List<AbilityScore>()
               );

        builder.Property(f => f.AbilityIncreases)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions)
               );

        // Owned Type: Requirements
        builder.OwnsOne(f => f.Requirements, fr =>
        {
            fr.Property(r => r.MinLevel);

            fr.Property(r => r.MinAbilityScores)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions)
              );

            fr.Property(r => r.RequiredWeaponProficiencies)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions)
              );

            fr.Property(r => r.RequiredArmorProficiencies)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions)
              );

            fr.Property(r => r.RequiresSpellcasting);
            
        });

        // Owned Collection: Effects
        builder.OwnsMany(f => f.Effects, fe =>
        {
            fe.WithOwner().HasForeignKey("FeatureId");
            fe.Property<Guid>("Id");
            fe.HasKey("Id");

            fe.Property(e => e.Type)
              .IsRequired();

            fe.Property(e => e.Target)
              .IsRequired();

            fe.Property(e => e.CustomTarget)
              .HasMaxLength(100);

            fe.Property(e => e.Value)
              .IsRequired()
              .HasMaxLength(500);

            fe.Property(e => e.Conditions)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, jsonOptions)
              );
        });

        // Owned Type: Usage
        builder.OwnsOne(f => f.Usage, fu =>
        {
            fu.Property(u => u.Type)
              .IsRequired();

            fu.Property(u => u.MaxUsesFormula)
              .HasMaxLength(200);

            fu.Property(u => u.CurrentUses);

            fu.Property(u => u.RechargeCondition)
              .HasMaxLength(500);
        });

        // Many-to-Many: Characters
        builder.HasMany(f => f.Characters)
               .WithMany(c => c.SelectedFeatures)
               .UsingEntity(j => j.ToTable("CharacterFeatures"));

        // Many-to-Many: Races
        builder.HasMany(f => f.Races)
               .WithMany(r => r.RacialFeatures)
               .UsingEntity(j => j.ToTable("RaceFeatures"));

        // Indexes for performance
        builder.HasIndex(f => f.Name);
        builder.HasIndex(f => f.SourceType);
        builder.HasIndex(f => f.IsSelectableAsFeat);
    }
}