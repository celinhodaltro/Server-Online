﻿using System;
using System.Collections.Generic;

namespace Data.Entities;

public sealed class AccountEntity
{
    public AccountEntity()
    {
        Players = new HashSet<PlayerEntity>();
    }

    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    public int PremiumTime { get; set; }
    public string Secret { get; set; }
    public bool AllowManyOnline { get; set; }
    public DateTime? BanishedAt { get; set; }
    public string BanishmentReason { get; set; }
    public uint? BannedBy { get; set; }

    public ICollection<PlayerEntity> Players { get; set; }
    public ICollection<AccountVipListEntity> VipList { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(EmailAddress) &&
               !string.IsNullOrWhiteSpace(Password);
    }
}