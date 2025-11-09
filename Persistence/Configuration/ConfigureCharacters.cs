using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configuration;

public class ConfigureCharacter : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        // Primary Key
        builder.HasKey(c => c.Id);

        // Indexes
        builder.HasIndex(c => c.UserId);
        builder.HasIndex(c => c.RaceId);
        builder.HasIndex(c => c.BackgroundId);

        // Relationships
        builder.HasOne(c => c.User)
               .WithMany()
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Race)
               .WithMany()
               .HasForeignKey(c => c.RaceId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Background)
               .WithMany(b => b.Characters)
               .HasForeignKey(c => c.BackgroundId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.SelectedFeatures)
               .WithMany()
               .UsingEntity(j => j.ToTable("CharacterFeatures"));

        // Simple properties
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.UpdatedAt)
               .IsRequired();

        // Owned Type: SubRace
        builder.OwnsOne(c => c.SubRace, subRace =>
        {
            subRace.Property(sr => sr.Name)
                   .HasMaxLength(100);

            subRace.Property(sr => sr.Description)
                   .HasMaxLength(2000);

            subRace.Property(sr => sr.Source)
                   .HasMaxLength(100);

            subRace.Property(sr => sr.AbilityScoreIncreases)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<Dictionary<AbilityScore, int>>(v, jsonOptions) ?? new Dictionary<AbilityScore, int>()
                   );

            subRace.Property(sr => sr.BonusLanguages)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
                   );

            subRace.Property(sr => sr.BonusWeaponProficiencies)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
                   );

            subRace.Property(sr => sr.BonusToolProficiencies)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
                   );

            subRace.Property(sr => sr.BonusSenses)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<Dictionary<SenseType, int>>(v, jsonOptions) ?? new Dictionary<SenseType, int>()
                   );

            subRace.Property(sr => sr.SubracialFeatures)
                   .HasColumnType("jsonb")
                   .HasConversion(
                       v => JsonSerializer.Serialize(v, jsonOptions),
                       v => JsonSerializer.Deserialize<List<Feature>>(v, jsonOptions) ?? new List<Feature>()
                   );
        });

        // Owned Type: CharacterProfile
        builder.OwnsOne(c => c.Profile, profile =>
        {
            profile.Property(p => p.Height);
            profile.Property(p => p.Weight);
            profile.Property(p => p.Eyes).HasMaxLength(50);
            profile.Property(p => p.Skin).HasMaxLength(50);
            profile.Property(p => p.Hair).HasMaxLength(50);
            profile.Property(p => p.AvatarUrl).HasMaxLength(500);
        });

        // Owned Type: CharacterStats
        builder.OwnsOne(c => c.Stats, stats =>
        {
            stats.Property(s => s.TotalLevel);
            stats.Property(s => s.ExperiencePoints);

            stats.Property(s => s.Languages)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
                 );

            stats.Property(s => s.ToolProficiencies)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
                 );

            stats.Property(s => s.WeaponProficiencies)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<List<WeaponProperty>>(v, jsonOptions) ?? new List<WeaponProperty>()
                 );

            stats.Property(s => s.ArmorProficiencies)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<List<ArmorType>>(v, jsonOptions) ?? new List<ArmorType>()
                 );

            stats.Property(s => s.Senses)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<Dictionary<SenseType, int>>(v, jsonOptions) ?? new Dictionary<SenseType, int>()
                 );

            // Owned Collection: Abilities
            stats.OwnsMany(s => s.Abilities, ability =>
            {
                ability.Property(a => a.Ability);
                ability.Property(a => a.AbilityValue);
                ability.Property(a => a.HasSavingThrowProficiency);

                // Owned Collection: Skills внутри Ability
                ability.OwnsMany(a => a.Skills, skill =>
                {
                    skill.Property(sk => sk.Skill);
                    skill.Property(sk => sk.Proficiency);
                });
            });
        });

        // Owned Type: CharacterCombat
        builder.OwnsOne(c => c.Combat, combat =>
        {
            combat.Property(cb => cb.MaxHitPoints);
            combat.Property(cb => cb.CurrentHitPoints);
            combat.Property(cb => cb.TemporaryHitPoints);
            combat.Property(cb => cb.ArmorClass);
            combat.Property(cb => cb.Speed);
            combat.Property(cb => cb.AttacksPerAction);
            combat.Property(cb => cb.DeathSaveSuccesses);
            combat.Property(cb => cb.DeathSaveFailures);
            combat.Property(cb => cb.HasInspiration);
            combat.Property(cb => cb.ExhaustionLevel);

            combat.Property(cb => cb.SpecialSpeeds)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<Dictionary<string, int>>(v, jsonOptions) ?? new Dictionary<string, int>()
                  );

            combat.Property(cb => cb.DamageResistances)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<DamageType>>(v, jsonOptions) ?? new List<DamageType>()
                  );

            combat.Property(cb => cb.DamageImmunities)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<DamageType>>(v, jsonOptions) ?? new List<DamageType>()
                  );

            combat.Property(cb => cb.DamageVulnerabilities)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<DamageType>>(v, jsonOptions) ?? new List<DamageType>()
                  );

            combat.Property(cb => cb.ConditionImmunities)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<Condition>>(v, jsonOptions) ?? new List<Condition>()
                  );

            combat.Property(cb => cb.CurrentConditions)
                  .HasColumnType("jsonb")
                  .HasConversion(
                      v => JsonSerializer.Serialize(v, jsonOptions),
                      v => JsonSerializer.Deserialize<List<Condition>>(v, jsonOptions) ?? new List<Condition>()
                  );
        });

        // Owned Type: CharacterMagic
        builder.OwnsOne(c => c.Magic, magic =>
        {
            magic.Property(m => m.ConcentratingOnSpellId);

            magic.Property(m => m.SpellcastingByClass)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<Dictionary<Guid, SpellcastingInfo>>(v, jsonOptions) ?? new Dictionary<Guid, SpellcastingInfo>()
                 );

            magic.Property(m => m.ClassResources)
                 .HasColumnType("jsonb")
                 .HasConversion(
                     v => JsonSerializer.Serialize(v, jsonOptions),
                     v => JsonSerializer.Deserialize<Dictionary<ResourceType, ResourcePool>>(v, jsonOptions) ?? new Dictionary<ResourceType, ResourcePool>()
                 );
        });

        // Owned Type: CharacterInventory
        builder.OwnsOne(c => c.Inventory, inventory =>
        {
            inventory.Property(i => i.TotalWeight);

            // Owned Type: Money
            inventory.OwnsOne(i => i.Money, money =>
            {
                money.Property(m => m.Copper);
                money.Property(m => m.Silver);
                money.Property(m => m.Electrum);
                money.Property(m => m.Gold);
                money.Property(m => m.Platinum);
            });

            // Owned Type: Equipment
            inventory.OwnsOne(i => i.Equipment, equipment =>
            {
                equipment.Property(e => e.MainHandWeaponId);
                equipment.Property(e => e.OffHandWeaponId);
                equipment.Property(e => e.ArmorId);
                equipment.Property(e => e.ShieldId);
                equipment.Property(e => e.MaxAttunedItems);

                equipment.Property(e => e.AccessorySlots)
                         .HasColumnType("jsonb")
                         .HasConversion(
                             v => JsonSerializer.Serialize(v, jsonOptions),
                             v => JsonSerializer.Deserialize<Dictionary<string, Guid>>(v, jsonOptions) ?? new Dictionary<string, Guid>()
                         );

                equipment.Property(e => e.AttunedItemIds)
                         .HasColumnType("jsonb")
                         .HasConversion(
                             v => JsonSerializer.Serialize(v, jsonOptions),
                             v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions) ?? new List<Guid>()
                         );
            });

            // Owned Collection: Items
            inventory.OwnsMany(i => i.Items, item =>
            {
                item.Property(it => it.ItemId);
                item.Property(it => it.Quantity);
                item.Property(it => it.IsEquipped);
                item.Property(it => it.IsAttuned);
            });
        });

        // Owned Type: CharacterPersonality
        builder.OwnsOne(c => c.Personality, personality =>
        {
            personality.Property(p => p.PersonalityTraits).HasMaxLength(2000);
            personality.Property(p => p.Ideals).HasMaxLength(2000);
            personality.Property(p => p.Bonds).HasMaxLength(2000);
            personality.Property(p => p.Flaws).HasMaxLength(2000);
            personality.Property(p => p.Backstory).HasMaxLength(10000);

            personality.Property(p => p.AlliesAndOrganizations)
                       .HasColumnType("jsonb")
                       .HasConversion(
                           v => JsonSerializer.Serialize(v, jsonOptions),
                           v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>()
                       );

            personality.Property(p => p.Enemies)
                       .HasColumnType("jsonb")
                       .HasConversion(
                           v => JsonSerializer.Serialize(v, jsonOptions),
                           v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>()
                       );
        });

        // Owned Collection: CharacterClass
        builder.OwnsMany(c => c.Classes, characterClass =>
        {
            characterClass.Property(cc => cc.Level);

            characterClass.Property(cc => cc.ChoicesByLevel)
                          .HasColumnType("jsonb")
                          .HasConversion(
                              v => JsonSerializer.Serialize(v, jsonOptions),
                              v => JsonSerializer.Deserialize<Dictionary<int, LevelChoices>>(v, jsonOptions) ?? new Dictionary<int, LevelChoices>()
                          );

            characterClass.Property(cc => cc.HitPointsByLevel)
                          .HasColumnType("jsonb")
                          .HasConversion(
                              v => JsonSerializer.Serialize(v, jsonOptions),
                              v => JsonSerializer.Deserialize<Dictionary<int, int>>(v, jsonOptions) ?? new Dictionary<int, int>()
                          );
        });

        // Owned Collection: HitDiceEntry
        builder.OwnsMany(c => c.HitDice, hitDice =>
        {
            hitDice.Property(hd => hd.DiceType);
            hitDice.Property(hd => hd.Total);
            hitDice.Property(hd => hd.Used);
        });

        // Owned Collection: CharacterAction
        builder.OwnsMany(c => c.AvailableActions, action =>
        {
            action.Property(a => a.Name).HasMaxLength(100);
            action.Property(a => a.Description).HasMaxLength(2000);
            action.Property(a => a.ActionType);
            action.Property(a => a.Source);
            action.Property(a => a.SourceId);
            action.Property(a => a.AttackBonus);
            action.Property(a => a.DamageDiceType);
            action.Property(a => a.DamageDiceCount);
            action.Property(a => a.DamageType);
            action.Property(a => a.DamageBonus);
            action.Property(a => a.SpellLevel);
            action.Property(a => a.Range).HasMaxLength(50);
            action.Property(a => a.UsageType);
            action.Property(a => a.UsesRemaining);
        });
    }
}