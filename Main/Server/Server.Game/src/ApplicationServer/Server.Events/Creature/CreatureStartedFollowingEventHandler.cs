﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts;
using Server.Tasks;

namespace Server.Events.Creature;

public class CreatureStartedFollowingEventHandler
{
    private readonly IDictionary<uint, uint> followEvents = new Dictionary<uint, uint>();
    private readonly IGameServer game;

    public CreatureStartedFollowingEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IWalkableCreature creature, ICreature following, FindPathParams fpp)
    {
        followEvents.TryGetValue(creature.CreatureId, out var followEvent);

        if (followEvent != 0) return;

        var eventId = game.Scheduler.AddEvent(new SchedulerEvent(1000, () => Follow(creature, fpp)));
        followEvents.AddOrUpdate(creature.CreatureId, eventId);
    }

    private void Follow(IWalkableCreature creature, FindPathParams fpp)
    {
        followEvents.TryGetValue(creature.CreatureId, out var followEvent);

        if (creature.IsFollowing)
        {
            creature.Follow(creature.Following);
        }
        else
        {
            if (followEvent != 0)
            {
                game.Scheduler.CancelEvent(followEvent);
                followEvent = 0;
                followEvents.Remove(creature.CreatureId);
            }
        }

        if (followEvent == 0) return;

        followEvents.Remove(creature.CreatureId);
        Execute(creature, creature.Following, fpp);
    }
}