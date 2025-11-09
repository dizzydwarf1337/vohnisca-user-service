using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureBackgrounds : IEntityTypeConfiguration<Background>
{
    public void Configure(EntityTypeBuilder<Background> builder)
    {
        var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false
};

builder.Property(b => b.SkillProficiencies)
       .HasConversion(
           v => JsonSerializer.Serialize(v, jsonOptions),
           v => JsonSerializer.Deserialize<List<Skill>>(v, jsonOptions) ?? new List<Skill>()
       );

builder.Property(b => b.ToolProficiencies)
       .HasConversion(
           v => JsonSerializer.Serialize(v, jsonOptions),
           v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
       );

builder.Property(b => b.GrantedLanguages)
       .HasConversion(
           v => JsonSerializer.Serialize(v, jsonOptions),
           v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
       );

builder.Property(b => b.StartingEquipment)
       .HasConversion(
           v => JsonSerializer.Serialize(v, jsonOptions),
           v => JsonSerializer.Deserialize<List<Item>>(v, jsonOptions) ?? new List<Item>()
       );

builder.OwnsOne(b => b.BackgroundFeature, bf =>
{
    bf.Property(f => f.Effects)
      .HasConversion(
          v => JsonSerializer.Serialize(v, jsonOptions),
          v => JsonSerializer.Deserialize<List<FeatureEffect>>(v, jsonOptions) ?? new List<FeatureEffect>()
      );

    bf.Property(f => f.AllowedAbilitiesForHalfASI)
      .HasConversion(
          v => JsonSerializer.Serialize(v, jsonOptions),
          v => JsonSerializer.Deserialize<List<AbilityScore>>(v, jsonOptions) ?? new List<AbilityScore>()
      );

    bf.Property(f => f.AbilityIncreases)
      .HasConversion(
          v => JsonSerializer.Serialize(v, jsonOptions),
          v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions) ?? new Dictionary<AbilityScore, int>()
      );

    bf.OwnsOne(f => f.Usage, fu =>
    {
        fu.Property(u => u.Type);
        fu.Property(u => u.MaxUsesFormula);
        fu.Property(u => u.CurrentUses);
        fu.Property(u => u.RechargeCondition);
    });

    bf.OwnsOne(f => f.Requirements, fr =>
    {
        fr.Property(r => r.MinAbilityScores)
          .HasConversion(
              v => JsonSerializer.Serialize(v, jsonOptions),
              v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions) ?? new Dictionary<AbilityScore, int>()
          );

        fr.Property(r => r.RequiredWeaponProficiencies)
          .HasConversion(
              v => JsonSerializer.Serialize(v, jsonOptions),
              v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
          );

        fr.Property(r => r.RequiredArmorProficiencies)
          .HasConversion(
              v => JsonSerializer.Serialize(v, jsonOptions),
              v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions) ?? new List<ArmorType>()
          );

        fr.Property(r => r.RequiredRaces)
          .HasConversion(
              v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), jsonOptions),
              v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions)!.Select(id => new Race { Id = id }).ToList()
          );

        fr.Property(r => r.RequiredFeatures)
          .HasConversion(
              v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), jsonOptions),
              v => JsonSerializer.Deserialize<List<Feature>>(v, jsonOptions)!
          );

        fr.Property(r => r.RequiredFeatures)
          .HasConversion(
              v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), jsonOptions),
              v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions)!.Select(id => new Feature { Id = id }).ToList()
          );
    });
});
    }
}