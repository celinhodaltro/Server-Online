﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;
using Data.Seeds;

namespace Data.Configurations;

public class PlayerItemEntityConfiguration : IEntityTypeConfiguration<Server.Entities.CharacterItem>
{
    public void Configure(EntityTypeBuilder<Server.Entities.CharacterItem> entity)
    {
        entity.ToTable("PlayerItem");

        entity.HasKey(x => x.Id);

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.CharacterId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.ServerId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.Amount)
            .HasColumnType("smallint(5)")
            .HasDefaultValueSql("1");

        entity.Property(e => e.ParentId)
            .HasColumnType("int(11)")
            .HasDefaultValueSql("0");

        entity.Property(e => e.ContainerId)
            .IsRequired()
            .HasColumnType("smallint(5)")
            .HasDefaultValueSql("0");

        entity.HasOne(d => d.Character)
            .WithMany(p => p.CharacterItems)
            .HasForeignKey(d => d.CharacterId)
            .HasConstraintName("player_items_ibfk_1");

        entity.Property(e => e.DecayTo)
            .HasColumnType("int");

        entity.Property(e => e.DecayDuration)
            .HasColumnType("int");

        entity.Property(e => e.DecayElapsed)
            .HasColumnType("int");

        entity.Property(e => e.Charges)
            .HasColumnType("int");

        PlayerItemSeed.Seed(entity);
    }
}