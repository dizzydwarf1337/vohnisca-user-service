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
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(2000);
        builder.Property(c => c.Source).HasMaxLength(100);
        builder.Property(c => c.IconUrl).HasMaxLength(200);

        builder.Property(c => c.HitDie);
        builder.Property(c => c.PrimaryStat);
        builder.Property(c => c.SkillChoiceCount);
        builder.Property(c => c.IsSpellcaster);
        builder.Property(c => c.SpellcastingType);
        builder.Property(c => c.SpellcastingAbility);
        builder.Property(c => c.IsPreparedCaster);
        builder.Property(c => c.SubclassLevel);
        
        builder.Property(c => c.SavingThrowProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<AbilityScore>>(v,  new JsonSerializerOptions())!);

        builder.Property(c => c.StartingArmorProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ArmorType>>(v,  new JsonSerializerOptions())!);

        builder.Property(c => c.StartingWeaponProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.StartingToolProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.AvailableSkills)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<Skill>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.LevelProgressions)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ClassLevelProgression>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.MulticlassPrerequisites)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.MulticlassArmorProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ArmorType>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.MulticlassWeaponProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, new JsonSerializerOptions())!);

        builder.Property(c => c.MulticlassToolProficiencies)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, new JsonSerializerOptions())!);
        
        builder.HasMany(c => c.AvailableSpells)
            .WithMany(s => s.AvailableForClasses);
    }
}