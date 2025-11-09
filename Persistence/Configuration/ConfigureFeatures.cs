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
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(f => f.Description)
               .HasMaxLength(1000);

        builder.Property(f => f.Source)
               .HasMaxLength(100);

        builder.Property(f => f.SourceType);
        builder.Property(f => f.Type);
        builder.Property(f => f.GrantsHalfASI);
        builder.Property(f => f.IsSelectableAsFeat);

        builder.Property(f => f.AllowedAbilitiesForHalfASI)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<AbilityScore>>(v, new JsonSerializerOptions()) ?? new List<AbilityScore>()
               );

        builder.Property(f => f.AbilityIncreases)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, new JsonSerializerOptions()) ?? new Dictionary<AbilityScore, int>()
               );

        builder.OwnsOne(f => f.Requirements, fr =>
        {
            fr.Property(r => r.MinLevel);

            fr.Property(r => r.MinAbilityScores)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, new JsonSerializerOptions()) ?? new Dictionary<AbilityScore, int>()
              );

            fr.Property(r => r.RequiredWeaponProficiencies)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, new JsonSerializerOptions()) ?? new List<WeaponProperty>()
              );

            fr.Property(r => r.RequiredArmorProficiencies)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<List<ArmorType>>(v, new JsonSerializerOptions()) ?? new List<ArmorType>()
              );

            fr.Property(r => r.RequiresSpellcasting);
        });

        builder.OwnsMany(f => f.Effects, fe =>
        {
            fe.Property(e => e.Type);
            fe.Property(e => e.Target);
            fe.Property(e => e.CustomTarget);
            fe.Property(e => e.Value);

            fe.Property(e => e.Conditions)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions()) ?? new Dictionary<string, string>()
              );
        });

        builder.OwnsOne(f => f.Usage, fu =>
        {
            fu.Property(u => u.Type);
            fu.Property(u => u.MaxUsesFormula);
            fu.Property(u => u.CurrentUses);
            fu.Property(u => u.RechargeCondition);
        });
    }
}