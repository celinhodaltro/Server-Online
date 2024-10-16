﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Helpers;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Events.Creature;

public class CreatureStartedWalkingEventHandler
{
    private readonly IDictionary<uint, uint> eventWalks = new Dictionary<uint, uint>();
    private readonly IGameServer game;

    public CreatureStartedWalkingEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IWalkableCreature creature)
    {
        eventWalks.TryGetValue(creature.CreatureId, out var eventWalk);

        if (eventWalk != 0) return;

        var eventId = game.Scheduler.AddEvent(new SchedulerEvent(creature.StepDelay, () => Move(creature)));
        eventWalks.AddOrUpdate(creature.CreatureId, eventId);
    }

    private void Move(IWalkableCreature creature)
    {
        eventWalks.TryGetValue(creature.CreatureId, out var eventWalk);

        if (creature.HasNextStep)
        {
            game.Map.MoveCreature(creature);
        }
        else
        {
            if (eventWalk != 0)
            {
                game.Scheduler.CancelEvent(eventWalk);

                eventWalk = 0;
                eventWalks.Remove(creature.CreatureId);
            }
        }

        if (eventWalk == 0) return;

        eventWalks.Remove(creature.CreatureId);
        Execute(creature);
    }
}