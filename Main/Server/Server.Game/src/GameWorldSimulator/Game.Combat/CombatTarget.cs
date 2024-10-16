﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Location;

namespace Game.Combat;

public class CombatTarget
{
    public CombatTarget(ICombatActor creature)
    {
        Creature = creature;
    }

    public ICombatActor Creature { get; }
    public Direction[] PathToCreature { get; private set; }
    public bool CanReachCreature { get; private set; } = true;
    public bool HasSightClear { get; private set; }

    public bool IsInRange(IMonster monster)
    {
        return Creature.Location.GetSqmDistance(monster.Location) <=
               monster.Metadata.MaxRangeDistanceAttack;
    }

    private void SetAsUnreachable()
    {
        CanReachCreature = false;
    }

    public void SetAsReachable(Direction[] path)
    {
        CanReachCreature = true;
        PathToCreature = path;
    }

    public void SetAsHasSightClear()
    {
        HasSightClear = true;
    }

    private void SetAsNoSightClear()
    {
        HasSightClear = false;
    }

    public void ResetFlags()
    {
        SetAsUnreachable();
        SetAsNoSightClear();
    }
}