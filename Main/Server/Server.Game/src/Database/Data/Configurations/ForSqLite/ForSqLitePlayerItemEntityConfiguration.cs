using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;
using Data.Seeds;

namespace Data.Configurations.ForSqLite;

public class ForSqLitePlayerItemEntityConfiguration : IEntityTypeConfiguration<Server.Entities.CharacterItem>
{
    public void Configure(EntityTypeBuilder<Server.Entities.CharacterItem> entity)
    {
        entity.ToTable("CharacterItem");

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasAnnotation("Sqlite:Autoincrement", true)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Amount)
            .IsRequired()
            .HasDefaultValueSql("1");

        entity.Property(e => e.ParentId)
            .HasDefaultValueSql("0");

        entity.Property(e => e.CharacterId)
            .IsRequired();

        entity.Property(e => e.ServerId)
            .IsRequired();

        entity.Property(e => e.ContainerId)
            .IsRequired()
            .HasDefaultValueSql("0");

        entity.HasOne(d => d.Character)
            .WithMany(p => p.CharacterItems)
            .HasForeignKey(d => d.CharacterId)
            .HasConstraintName("Character_items_ibfk_1");

        entity.Property(e => e.DecayTo)
            .HasDefaultValueSql("0");

        entity.Property(e => e.DecayDuration)
            .HasDefaultValueSql("0");

        entity.Property(e => e.DecayElapsed)
            .HasDefaultValueSql("0");

        entity.Property(e => e.Charges)
            .HasDefaultValueSql("0");

        PlayerItemSeed.Seed(entity);
    }
}