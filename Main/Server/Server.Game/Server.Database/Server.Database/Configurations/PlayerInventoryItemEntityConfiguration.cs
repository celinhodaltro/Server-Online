﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;
using Data.Seeds;

namespace Data.Configurations;

public class PlayerInventoryItemEntityConfiguration : IEntityTypeConfiguration<PlayerInventoryItemEntity>
{
    public void Configure(EntityTypeBuilder<PlayerInventoryItemEntity> entity)
    {
        entity.HasKey(e => e.Id);

        entity.ToTable("PlayerInventoryItem");

        entity.Property(e => e.PlayerId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.ServerId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.SlotId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Amount)
            .HasColumnType("smallint(5)")
            .HasDefaultValueSql("1");

        entity.HasOne(d => d.Player)
            .WithMany(p => p.PlayerInventoryItems)
            .HasForeignKey(d => d.PlayerId)
            .HasConstraintName("player_inventory_items_ibfk_1");

        PlayerInventorySeed.Seed(entity);
    }
}