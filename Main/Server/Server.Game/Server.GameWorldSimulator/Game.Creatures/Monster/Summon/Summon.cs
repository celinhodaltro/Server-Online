﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;

namespace Game.Creatures.Monster.Summon;

public class Summon : Monster
{
    public Summon(IMonsterType type, IMapTool mapTool, ICreature master) : base(type, mapTool, null)
    {
        Master = master;
        if (master is not ICombatActor actor) return;

        actor.OnKilled += OnMasterKilled;
        actor.OnTargetChanged += OnMasterTargetChange;
    }

    public ICreature Master { get; }
    public override bool IsSummon => true;

    public override void SetAsEnemy(ICreature creature)
    {
        if (IsDead) return;
        if (Master == creature) return;

        if (creature is Summon summon && summon.Master == Master) return;

        base.SetAsEnemy(creature);
    }

    private void Die()
    {
        HealthPoints = 0;
        OnDeath(this);
    }

    public override void OnDeath(IThing by)
    {
        base.OnDeath(by);

        if (Master is not ICombatActor actor) return;
        actor.OnKilled -= OnMasterKilled;
        actor.OnTargetChanged -= OnMasterTargetChange;
    }

    private void OnMasterKilled(ICombatActor master, IThing by, ILoot loot)
    {
        master.OnKilled -= OnMasterKilled;
        master.OnTargetChanged -= OnMasterTargetChange;
        Die();
    }

    private void OnMasterTargetChange(ICombatActor actor, uint oldTargetId, uint newTargetId)
    {
        Targets.Clear();
        SetAsEnemy(actor.CurrentTarget);
    }
}