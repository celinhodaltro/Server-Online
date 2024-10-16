﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations;

public class PlayerOutfitAddonEntityConfiguration : IEntityTypeConfiguration<PlayerOutfitAddonEntity>
{
    public void Configure(EntityTypeBuilder<PlayerOutfitAddonEntity> builder)
    {
        builder.ToTable("PlayerOutfitAddon");

        builder.HasKey(e => new { e.PlayerId, e.LookType, e.AddonLevel });

        builder.Property(e => e.LookType).IsRequired();
        builder.Property(e => e.PlayerId).IsRequired();
        builder.Property(e => e.AddonLevel).IsRequired();
    }
}