using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Domain.Models.Characters.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureItems : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(i => i.Description)
               .HasMaxLength(2000);

        builder.Property(i => i.Source)
               .HasMaxLength(100);

        builder.Property(i => i.Weight);

        builder.Property(i => i.ValueInCopper);

        builder.Property(i => i.Rarity);

        builder.Property(i => i.Properties)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ItemProperty>>(v, jsonOptions) ?? new List<ItemProperty>()
               );

        builder.HasMany(i => i.Backgrounds)
               .WithMany(b => b.StartingEquipment);

        builder.OwnsOne(i => i.Requirements, ir =>
        {
            ir.Property(r => r.MinLevel);

            ir.Property(r => r.MinAbilityScores)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions)
              );

            ir.Property(r => r.RequiredWeaponProficiencies)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions)
              );

            ir.Property(r => r.RequiredArmorProficiencies)
              .HasColumnType("jsonb")
              .HasConversion(
                  v => JsonSerializer.Serialize(v, jsonOptions),
                  v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions)
              );

            ir.Property(r => r.RequiresSpellcasting);

            ir.Property(r => r.RequiredRaces)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v == null ? new List<Guid>() : v.Select(x => x.Id).ToList(), jsonOptions),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions).Select(id => new Race { Id = id }).ToList()
                );

            ir.Property(r => r.RequiredClasses)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v == null ? new List<Guid>() : v.Select(x => x.Id).ToList(), jsonOptions),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions).Select(id => new Class { Id = id }).ToList()
                );

            ir.Property(r => r.RequiredFeatures)
                .HasColumnType("jsonb")
                .HasConversion(
                    v => JsonSerializer.Serialize(v == null ? new List<Guid>() : v.Select(x => x.Id).ToList(), jsonOptions),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions).Select(id => new Feature { Id = id }).ToList()
                );
        });

        // TPH - Table Per Hierarchy inheritance
        builder.HasDiscriminator<string>("ItemType")
               .HasValue<Item>("Item")
               .HasValue<Weapon>("Weapon")
               .HasValue<Armor>("Armor");

        builder.HasIndex(i => i.Name);
        builder.HasIndex(i => i.Rarity);
    }
}

public class ConfigureWeapons : IEntityTypeConfiguration<Weapon>
{
    public void Configure(EntityTypeBuilder<Weapon> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        builder.Property(w => w.DamageDice);
        builder.Property(w => w.DamageDiceCount);
        builder.Property(w => w.DamageType);
        builder.Property(w => w.RangeNormal);
        builder.Property(w => w.RangeLong);
        builder.Property(w => w.VersatileDiceType);
        builder.Property(w => w.VersatileDiceCount);
        builder.Property(w => w.IsMagical);
        builder.Property(w => w.MagicBonus);

        builder.Property(w => w.WeaponProperties)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
               );
    }
}

public class ConfigureArmors : IEntityTypeConfiguration<Armor>
{
    public void Configure(EntityTypeBuilder<Armor> builder)
    {
        builder.Property(a => a.ArmorType);
        builder.Property(a => a.BaseArmorClass);
        builder.Property(a => a.AddDexModifier);
        builder.Property(a => a.MaxDexModifier);
        builder.Property(a => a.StrengthRequirement);
        builder.Property(a => a.HasStealthDisadvantage);
        builder.Property(a => a.IsMagical);
        builder.Property(a => a.MagicBonus);
    }
}