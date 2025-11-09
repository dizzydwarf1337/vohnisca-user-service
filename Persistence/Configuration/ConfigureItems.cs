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
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(i => i.Description)
               .HasMaxLength(1000);

        builder.Property(i => i.Source)
               .HasMaxLength(100);

        builder.Property(i => i.Weight);
        builder.Property(i => i.ValueInCopper);
        builder.Property(i => i.Rarity);

        builder.Property(i => i.Properties)
               .HasConversion(
                   v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                   v => JsonSerializer.Deserialize<List<ItemProperty>>(v, new JsonSerializerOptions()) ?? new List<ItemProperty>()
               );

        builder.HasMany(i => i.Backgrounds)
               .WithMany();

        builder.OwnsOne(i => i.Requirements, ir =>
        {
            ir.Property(r => r.MinLevel);

            ir.Property(r => r.MinAbilityScores)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, new JsonSerializerOptions()) ?? new Dictionary<AbilityScore, int>()
              );

            ir.Property(r => r.RequiredWeaponProficiencies)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, new JsonSerializerOptions()) ?? new List<WeaponProperty>()
              );

            ir.Property(r => r.RequiredArmorProficiencies)
              .HasConversion(
                  v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                  v => JsonSerializer.Deserialize<List<ArmorType>>(v, new JsonSerializerOptions()) ?? new List<ArmorType>()
              );

            ir.Property(r => r.RequiresSpellcasting);

            ir.Property(r => r.RequiredRaces)
                .HasConversion(
                    v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), new JsonSerializerOptions()),
                    v => v == null ? new List<Race>() : JsonSerializer.Deserialize<List<Guid>>(v, new JsonSerializerOptions())!.Select(id => new Race { Id = id }).ToList()
                );

            ir.Property(r => r.RequiredClasses)
                .HasConversion(
                    v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), new JsonSerializerOptions()),
                    v => v == null ? new List<Class>() : JsonSerializer.Deserialize<List<Guid>>(v, new JsonSerializerOptions())!.Select(id => new Class { Id = id }).ToList()
                );

            ir.Property(r => r.RequiredFeatures)
                .HasConversion(
                    v => JsonSerializer.Serialize(v.Select(x => x.Id).ToList(), new JsonSerializerOptions()),
                    v => v == null ? new List<Feature>() : JsonSerializer.Deserialize<List<Guid>>(v, new JsonSerializerOptions())!.Select(id => new Feature { Id = id }).ToList()
                );
        });

        // Конфигурация наследников
        builder.HasDiscriminator<string>("ItemType")
               .HasValue<Item>("Item")
               .HasValue<Weapon>("Weapon")
               .HasValue<Armor>("Armor");

        builder.OwnsOne<Weapon>("Weapon", w =>
        {
            w.Property(we => we.DamageDice);
            w.Property(we => we.DamageDiceCount);
            w.Property(we => we.DamageType);
            w.Property(we => we.WeaponProperties)
             .HasConversion(
                 v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                 v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, new JsonSerializerOptions()) ?? new List<WeaponProperty>()
             );
            w.Property(we => we.RangeNormal);
            w.Property(we => we.RangeLong);
            w.Property(we => we.VersatileDiceType);
            w.Property(we => we.VersatileDiceCount);
            w.Property(we => we.IsMagical);
            w.Property(we => we.MagicBonus);
        });

        builder.OwnsOne<Armor>("Armor", a =>
        {
            a.Property(ar => ar.ArmorType);
            a.Property(ar => ar.BaseArmorClass);
            a.Property(ar => ar.AddDexModifier);
            a.Property(ar => ar.MaxDexModifier);
            a.Property(ar => ar.StrengthRequirement);
            a.Property(ar => ar.HasStealthDisadvantage);
            a.Property(ar => ar.IsMagical);
            a.Property(ar => ar.MagicBonus);
        });
    }
}