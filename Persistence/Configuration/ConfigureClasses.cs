using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Leveling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureClasses : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        };

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Description)
               .HasMaxLength(2000);

        builder.Property(c => c.Source)
               .HasMaxLength(100);

        builder.Property(c => c.IconUrl)
               .HasMaxLength(500);

        builder.Property(c => c.HitDie)
               .IsRequired();

        builder.Property(c => c.PrimaryStat)
               .IsRequired();

        builder.Property(c => c.SkillChoiceCount);

        builder.Property(c => c.IsSpellcaster);

        builder.Property(c => c.SpellcastingType);

        builder.Property(c => c.SpellcastingAbility);

        builder.Property(c => c.IsPreparedCaster);

        builder.Property(c => c.SubclassLevel);

        builder.Property(c => c.MulticlassSkillChoiceCount);

        builder.Property(c => c.SavingThrowProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<AbilityScore>>(v, jsonOptions) ?? new List<AbilityScore>()
               );

        builder.Property(c => c.StartingArmorProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions) ?? new List<ArmorType>()
               );

        builder.Property(c => c.StartingWeaponProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
               );

        builder.Property(c => c.StartingToolProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
               );

        builder.Property(c => c.AvailableSkills)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<Skill>>(v, jsonOptions) ?? new List<Skill>()
               );

        builder.Property(c => c.MulticlassPrerequisites)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions)
               );

        builder.Property(c => c.MulticlassArmorProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions)
               );

        builder.Property(c => c.MulticlassWeaponProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions)
               );

        builder.Property(c => c.MulticlassToolProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions)
               );
        
        builder.Property(c => c.LevelProgressions)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ClassLevelProgression>>(v, jsonOptions) ?? new List<ClassLevelProgression>()
               );

        // Owned Collection: Subclasses
        builder.OwnsMany(c => c.Subclasses, subclass =>
        {
            subclass.Property(sc => sc.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            subclass.Property(sc => sc.Description)
                    .HasMaxLength(2000);

            subclass.Property(sc => sc.Source)
                    .HasMaxLength(100);

            subclass.Property(sc => sc.IconUrl)
                    .HasMaxLength(500);

            subclass.Property(sc => sc.BonusArmorProficiencies)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions)
                    );

            subclass.Property(sc => sc.BonusWeaponProficiencies)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions)
                    );

            subclass.Property(sc => sc.BonusToolProficiencies)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions)
                    );

            subclass.Property(sc => sc.LevelProgressions)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<List<ClassLevelProgression>>(v, jsonOptions) ?? new List<ClassLevelProgression>()
                    );

            subclass.Property(sc => sc.BonusSpellsByLevel)
                    .HasColumnType("jsonb")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, jsonOptions),
                        v => JsonSerializer.Deserialize<Dictionary<int, List<Guid>>>(v, jsonOptions)
                    );
        });

        builder.HasMany(c => c.AvailableSpells)
               .WithMany(s => s.AvailableForClasses);

        builder.HasIndex(c => c.Name);
    }
}