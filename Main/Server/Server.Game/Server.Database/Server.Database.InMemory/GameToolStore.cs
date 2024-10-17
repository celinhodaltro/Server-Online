﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;

namespace Data.InMemory;

public class GameToolStore : DataStore<GameToolStore, string, object>
{
    public static IPathFinder PathFinder
    {
        get => Data.Get(nameof(PathFinder)) is not IPathFinder pathFinder ? null : pathFinder;
        set => Data.Add(nameof(PathFinder), value);
    }

    public static IWalkToMechanism WalkToMechanism
    {
        get => Data.Get(nameof(WalkToMechanism)) is not IWalkToMechanism walkToMechanism ? null : walkToMechanism;
        set => Data.Add(nameof(WalkToMechanism), value);
    }
}