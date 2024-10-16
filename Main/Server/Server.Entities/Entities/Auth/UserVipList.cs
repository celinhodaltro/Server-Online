﻿namespace Server.Entities;
public sealed class UserVipList : DefaultDb
{
    public int AccountId { get; set; }
    public int PlayerId { get; set; }
    public string Description { get; set; }
    public Character Player { get; set; }
}