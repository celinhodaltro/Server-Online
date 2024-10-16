﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Creature;
using Networking.Packets.Outgoing.Effect;
using Networking.Packets.Outgoing.Item;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Server.Events.Creature;

public class CreatureAddedOnMapEventHandler : IEventHandler
{
    private readonly IGameServer game;

    public CreatureAddedOnMapEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IWalkableCreature creature, ICylinder cylinder)
    {
        if (Guard.AnyNull(cylinder, cylinder.TileSpectators, creature)) return;

        var tile = cylinder.ToTile;
        if (tile.IsNull()) return;

        foreach (var cylinderSpectator in cylinder.TileSpectators)
        {
            var spectator = cylinderSpectator.Spectator;

            if (spectator is not IPlayer spectatorPlayer) continue;
            if (Equals(creature, spectator)) continue;

            if (!spectator.CanSee(creature.Location)) continue;

            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            SendPacketsToSpectator(spectatorPlayer, creature, connection,
                cylinderSpectator.ToStackPosition == byte.MaxValue
                    ? cylinderSpectator.FromStackPosition
                    : cylinderSpectator.ToStackPosition);

            connection.Send();
        }
    }

    private static void SendPacketsToSpectator(IPlayer playerToSend, IWalkableCreature creatureAdded,
        IConnection connection, byte stackPosition)
    {
        connection.OutgoingPackets.Enqueue(new AddAtStackPositionPacket(creatureAdded, stackPosition));
        connection.OutgoingPackets.Enqueue(new AddCreaturePacket(playerToSend, creatureAdded));
        connection.OutgoingPackets.Enqueue(new MagicEffectPacket(creatureAdded.Location, EffectT.BubbleBlue));
    }
}