﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations;

public class GuildEntityConfiguration : IEntityTypeConfiguration<GuildEntity>
{
    public void Configure(EntityTypeBuilder<GuildEntity> builder)
    {
        builder.ToTable("Guild");

        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name);
        builder.Property(e => e.OwnerId);
        builder.Property(e => e.CreatedAt).HasDefaultValue(DateTime.UtcNow);
        builder.Property(e => e.Modt);

        builder.HasMany(x => x.Members).WithOne().HasForeignKey(x => x.GuildId);
        builder.HasMany(x => x.Ranks).WithOne().HasForeignKey(x => x.GuildId);
    }
}