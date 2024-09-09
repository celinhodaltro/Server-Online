﻿using Game.Common.Creatures.Players;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IPlayerOutFit
{
    public Gender Type { get; init; }
    public ushort LookType { get; init; }
    public string Name { get; init; }
    public bool RequiresPremium { get; init; }
    public bool Unlocked { get; init; }
    public bool Enabled { get; init; }
}