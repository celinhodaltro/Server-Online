﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations.ForSqLite;

public class ForSqLiteGuildRankEntityConfiguration : IEntityTypeConfiguration<GuildRankEntity>
{
    public void Configure(EntityTypeBuilder<GuildRankEntity> builder)
    {
        builder.ToTable("GuildRank");

        builder.HasKey(e => new { e.Id });

        builder.Property(e => e.Id).HasAnnotation("Sqlite:Autoincrement", true);
        builder.Property(e => e.GuildId);
        builder.Property(e => e.Name);
        builder.Property(e => e.Level);

        builder.HasOne(x => x.Guild);
    }
}