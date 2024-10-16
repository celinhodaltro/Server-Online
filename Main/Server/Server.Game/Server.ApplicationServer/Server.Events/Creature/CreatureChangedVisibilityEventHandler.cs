﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Networking.Packets.Outgoing.Creature;
using Networking.Packets.Outgoing.Item;
using Server.Common.Contracts;

namespace Server.Events.Creature;

public class CreatureChangedVisibilityEventHandler
{
    private readonly IGameServer game;
    private readonly IMap map;

    public CreatureChangedVisibilityEventHandler(IMap map, IGameServer game)
    {
        this.map = map;
        this.game = game;
    }

    public void Execute(IWalkableCreature creature)
    {
        foreach (var spectator in map.GetPlayersAtPositionZone(creature.Location))
        {
            if (ReferenceEquals(spectator, creature)) continue;

            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            if (!creature.Tile.TryGetStackPositionOfThing((IPlayer)spectator, creature, out var stackPosition))
                continue;

            if (!spectator.CanSee(creature.Location)) continue;

            if (creature.IsInvisible)
            {
                connection.OutgoingPackets.Enqueue(new RemoveTileThingPacket(creature.Tile, stackPosition));
            }
            else
            {
                connection.OutgoingPackets.Enqueue(new AddAtStackPositionPacket(creature, stackPosition));
                connection.OutgoingPackets.Enqueue(new AddCreaturePacket((IPlayer)spectator, creature));
            }

            connection.Send();
        }
    }
}