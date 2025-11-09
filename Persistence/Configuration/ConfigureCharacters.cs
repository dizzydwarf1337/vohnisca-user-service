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
            WriteIndented = false,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
        };
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.UpdatedAt)
               .IsRequired();

        builder.Property(c => c.Alignment)
               .IsRequired();

        builder.Property(c => c.Size)
               .IsRequired();

        builder.Property(c => c.Age);
        
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
               .WithMany(f => f.Characters)
               .UsingEntity(j => j.ToTable("CharacterFeatures"));
        
        builder.HasIndex(c => c.UserId);
        builder.HasIndex(c => c.RaceId);
        builder.HasIndex(c => c.BackgroundId);
        builder.HasIndex(c => c.Name);

        builder.OwnsOne(c => c.SubRace, subRace =>
        {
            subRace.Property(sr => sr.Name)
                   .HasMaxLength(100);

            subRace.Property(sr => sr.Description)
                   .HasMaxLength(2000);

            subRace.Property(sr => sr.Source)
                   .HasMaxLength(100);

            subRace.Property(sr => sr.SpeedOverride);

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
                       v => JsonSerializer.Serialize(v.Select(f => f.Id).ToList(), jsonOptions),
                       v => JsonSerializer.Deserialize<List<Guid>>(v, jsonOptions).Select(id => new Feature { Id = id }).ToList() ?? new List<Feature>()
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
        ConfigureCharacterStats(builder, jsonOptions);

        // Owned Type: CharacterCombat
        ConfigureCharacterCombat(builder, jsonOptions);

        // Owned Type: CharacterMagic
        ConfigureCharacterMagic(builder, jsonOptions);

        // Owned Type: CharacterInventory
        ConfigureCharacterInventory(builder, jsonOptions);

        // Owned Type: CharacterPersonality
        ConfigureCharacterPersonality(builder, jsonOptions);

        // Owned Collection: CharacterClass
        ConfigureCharacterClasses(builder, jsonOptions);

        // Owned Collection: HitDiceEntry
        builder.OwnsMany(c => c.HitDice, hitDice =>
        {
            hitDice.WithOwner().HasForeignKey("CharacterId");
            hitDice.Property<Guid>("Id");
            hitDice.HasKey("Id");

            hitDice.Property(hd => hd.DiceType)
                   .IsRequired();
            hitDice.Property(hd => hd.Total)
                   .IsRequired();
            hitDice.Property(hd => hd.Used)
                   .IsRequired();
        });

        // Owned Collection: CharacterAction
        builder.OwnsMany(c => c.AvailableActions, action =>
        {
            action.WithOwner().HasForeignKey("CharacterId");
            action.Property<Guid>("Id");
            action.HasKey("Id");

            action.Property(a => a.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            action.Property(a => a.Description)
                  .HasMaxLength(2000);
            action.Property(a => a.ActionType)
                  .IsRequired();
            action.Property(a => a.Source)
                  .IsRequired();
            action.Property(a => a.SourceId)
                  .IsRequired();
            action.Property(a => a.AttackBonus);
            action.Property(a => a.DamageDiceType);
            action.Property(a => a.DamageDiceCount);
            action.Property(a => a.DamageType);
            action.Property(a => a.DamageBonus);
            action.Property(a => a.SpellLevel);
            action.Property(a => a.Range)
                  .HasMaxLength(50);
            action.Property(a => a.UsageType)
                  .IsRequired();
            action.Property(a => a.UsesRemaining);
        });
    }

    private static void ConfigureCharacterStats(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
        builder.OwnsOne(c => c.Stats, stats =>
        {
            stats.Property(s => s.TotalLevel)
                 .IsRequired();
            stats.Property(s => s.ExperiencePoints)
                 .IsRequired();

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
                ability.WithOwner().HasForeignKey("CharacterStatsId");
                ability.Property<Guid>("Id");
                ability.HasKey("Id");

                ability.Property(a => a.Ability)
                       .IsRequired();
                ability.Property(a => a.AbilityValue)
                       .IsRequired();
                ability.Property(a => a.HasSavingThrowProficiency)
                       .IsRequired();

                // Owned Collection: Skills inside Ability
                ability.OwnsMany(a => a.Skills, skill =>
                {
                    skill.WithOwner().HasForeignKey("CharacterAbilityId");
                    skill.Property<Guid>("Id");
                    skill.HasKey("Id");

                    skill.Property(sk => sk.Skill)
                         .IsRequired();
                    skill.Property(sk => sk.Proficiency)
                         .IsRequired();
                });
            });
        });
    }

    private static void ConfigureCharacterCombat(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
        builder.OwnsOne(c => c.Combat, combat =>
        {
            combat.Property(cb => cb.MaxHitPoints)
                  .IsRequired();
            combat.Property(cb => cb.CurrentHitPoints)
                  .IsRequired();
            combat.Property(cb => cb.TemporaryHitPoints);
            combat.Property(cb => cb.ArmorClass)
                  .IsRequired();
            combat.Property(cb => cb.Speed)
                  .IsRequired();
            combat.Property(cb => cb.AttacksPerAction)
                  .IsRequired();
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
    }

    private static void ConfigureCharacterMagic(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
        builder.OwnsOne(c => c.Magic, magic =>
        {
            magic.Property(m => m.ConcentratingOnSpellId);

            magic.HasOne(m => m.ConcentratingOnSpell)
                 .WithMany()
                 .HasForeignKey(m => m.ConcentratingOnSpellId)
                 .OnDelete(DeleteBehavior.SetNull);

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
    }

    private static void ConfigureCharacterInventory(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
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
                equipment.Property(e => e.MaxAttunedItems)
                         .IsRequired();

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
                item.WithOwner().HasForeignKey("CharacterInventoryId");
                item.Property<Guid>("Id");
                item.HasKey("Id");

                item.Property(it => it.ItemId)
                    .IsRequired();
                item.Property(it => it.Quantity)
                    .IsRequired();
                item.Property(it => it.IsEquipped);
                item.Property(it => it.IsAttuned);

                // Navigation to Item
                item.HasOne(it => it.Item)
                    .WithMany()
                    .HasForeignKey(it => it.ItemId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        });
    }

    private static void ConfigureCharacterPersonality(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
        builder.OwnsOne(c => c.Personality, personality =>
        {
            personality.Property(p => p.PersonalityTraits)
                       .HasMaxLength(2000);
            personality.Property(p => p.Ideals)
                       .HasMaxLength(2000);
            personality.Property(p => p.Bonds)
                       .HasMaxLength(2000);
            personality.Property(p => p.Flaws)
                       .HasMaxLength(2000);
            personality.Property(p => p.Backstory)
                       .HasMaxLength(10000);

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
    }

    private static void ConfigureCharacterClasses(EntityTypeBuilder<Character> builder, JsonSerializerOptions jsonOptions)
    {
        builder.OwnsMany(c => c.Classes, characterClass =>
        {
            characterClass.WithOwner().HasForeignKey("CharacterId");
            characterClass.Property<Guid>("Id");
            characterClass.HasKey("Id");

            characterClass.Property(cc => cc.ClassId)
                          .IsRequired();

            characterClass.Property(cc => cc.Level)
                          .IsRequired();

            characterClass.Property(cc => cc.SubClassId);

            // Navigation to Class
            characterClass.HasOne(cc => cc.Class)
                          .WithMany()
                          .HasForeignKey(cc => cc.ClassId)
                          .OnDelete(DeleteBehavior.Restrict);

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
    }
}