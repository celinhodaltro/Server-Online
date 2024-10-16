﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations;

public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.ToTable("Account");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.EmailAddress)
            .IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.CreatedAt);

        builder.Property(e => e.EmailAddress)
            .IsRequired()
            .HasColumnType("varchar(320)");

        builder.Property(e => e.AllowManyOnline)
            .HasDefaultValue(0);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnType("char(20)");

        builder.Property(e => e.PremiumTime)
            .HasDefaultValueSql("0");

        builder.Property(e => e.Secret)
            .HasColumnType("char(16)");

        builder.HasMany(x => x.VipList)
            .WithOne()
            .HasForeignKey(x => x.AccountId);

        builder.Property(e => e.BanishedAt);

        builder.Property(e => e.BanishmentReason);

        builder.Property(e => e.BannedBy);

        Seed(builder);
    }

    private static void Seed(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.HasData
        (
            new AccountEntity
            {
                Id = 1,
                EmailAddress = "1",
                Password = "1",
                PremiumTime = 30,
                AllowManyOnline = true
            }
        );
    }
}