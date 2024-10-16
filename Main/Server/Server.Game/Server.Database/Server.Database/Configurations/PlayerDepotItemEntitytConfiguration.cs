﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations;

public class PlayerDepotItemEntitytConfiguration : IEntityTypeConfiguration<PlayerDepotItemEntity>
{
    public void Configure(EntityTypeBuilder<PlayerDepotItemEntity> entity)
    {
        entity.ToTable("PlayerDepotItem");

        entity.HasKey(x => x.Id);

        entity.Property(e => e.PlayerId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.ServerId)
            .IsRequired()
            .HasColumnType("int(11)");

        entity.Property(e => e.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        entity.Property(e => e.Amount)
            .IsRequired()
            .HasColumnType("smallint(5)")
            .HasDefaultValueSql("1");

        entity.Property(e => e.ParentId)
            .HasColumnType("int(11)")
            .HasDefaultValueSql("0");

        entity.HasOne(d => d.Player)
            .WithMany(p => p.PlayerDepotItems)
            .HasForeignKey(d => d.PlayerId)
            .HasConstraintName("player_depot_items_ibfk_1");

        entity.Property(e => e.DecayTo)
            .HasColumnType("int");

        entity.Property(e => e.DecayDuration)
            .HasColumnType("int");

        entity.Property(e => e.DecayElapsed)
            .HasColumnType("int");

        entity.Property(e => e.Charges)
            .HasColumnType("int");
    }
}