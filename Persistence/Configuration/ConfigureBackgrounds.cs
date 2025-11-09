using System.Text.Json;
using Domain.Models.Characters;
using Domain.Models.Characters.Enums;
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

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(b => b.Description)
               .HasMaxLength(2000);

        builder.Property(b => b.Source)
               .HasMaxLength(100);

        builder.Property(b => b.AvailableLanguages);

        builder.Property(b => b.SkillProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<Skill>>(v, jsonOptions) ?? new List<Skill>()
               );

        builder.Property(b => b.ToolProficiencies)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<ToolProficiency>>(v, jsonOptions) ?? new List<ToolProficiency>()
               );

        builder.Property(b => b.GrantedLanguages)
               .HasColumnType("jsonb")
               .HasConversion(
                   v => JsonSerializer.Serialize(v, jsonOptions),
                   v => JsonSerializer.Deserialize<List<Language>>(v, jsonOptions) ?? new List<Language>()
               );

        builder.OwnsOne(b => b.StartingMoney, money =>
        {
            money.Property(m => m.Copper);
            money.Property(m => m.Silver);
            money.Property(m => m.Electrum);
            money.Property(m => m.Gold);
            money.Property(m => m.Platinum);
        });

        builder.HasOne(b => b.BackgroundFeature)
               .WithMany()
               .HasForeignKey(b => b.BackgroundFeatureId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.StartingEquipment)
               .WithMany(i => i.Backgrounds);

        builder.HasMany(b => b.Characters)
               .WithOne(c => c.Background)
               .HasForeignKey(c => c.BackgroundId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}