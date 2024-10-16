﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Results;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Events.Combat;

public class CreatureChangedAttackTargetEventHandler
{
    private readonly IGameServer game;

    public CreatureChangedAttackTargetEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICombatActor actor, uint oldTarget, uint newTarget)
    {
        if (actor.AttackEvent != 0) return;

        var result = Attack(actor);
        var attackSpeed = result ? actor.AttackSpeed : 300;
        actor.AttackEvent = game.Scheduler.AddEvent(new SchedulerEvent((int)attackSpeed, () => Attack(actor)));
    }

    private bool Attack(ICombatActor actor)
    {
        var result = Result.NotPossible;

        if (actor.Attacking)
        {
            game.CreatureManager.TryGetCreature(actor.AutoAttackTargetId, out var creature);

            result = creature is not ICombatActor enemy ? Result.NotPossible : actor.Attack(enemy);
        }
        else
        {
            if (actor.AttackEvent != 0)
            {
                game.Scheduler.CancelEvent(actor.AttackEvent);
                actor.AttackEvent = 0;
            }
        }

        if (actor.AttackEvent == 0) return result.Succeeded;

        actor.AttackEvent = 0;
        Execute(actor, 0, 0);

        return result.Succeeded;
    }
}