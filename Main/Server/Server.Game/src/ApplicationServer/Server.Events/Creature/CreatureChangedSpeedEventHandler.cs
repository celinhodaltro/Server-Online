﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Networking.Packets.Outgoing.Creature;
using Server.Common.Contracts;

namespace Server.Events.Creature;

public class CreatureChangedSpeedEventHandler
{
    private readonly IGameServer game;
    private readonly IMap map;

    public CreatureChangedSpeedEventHandler(IMap map, IGameServer game)
    {
        this.map = map;
        this.game = game;
    }

    public void Execute(IWalkableCreature creature, ushort speed)
    {
        foreach (var spectator in map.GetPlayersAtPositionZone(creature.Location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;
            connection.OutgoingPackets.Enqueue(new CreatureChangeSpeedPacket(creature.CreatureId, speed));
            connection.Send();
        }
    }
}