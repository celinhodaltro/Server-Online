﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Effect;
using Networking.Packets.Outgoing.Item;
using Server.Common.Contracts;

namespace Server.Events.Tiles;

public class ThingRemovedFromTileEventHandler
{
    private readonly IGameServer game;

    public ThingRemovedFromTileEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IThing thing, ICylinder cylinder)
    {
        if (Guard.AnyNull(cylinder, cylinder.TileSpectators, thing)) return;

        var tile = cylinder.FromTile;
        if (tile.IsNull()) return;

        foreach (var spectator in cylinder.TileSpectators)
        {
            var creature = spectator.Spectator;

            if (creature is not IPlayer player) continue;
            if (!creature.CanSee(thing.Location)) continue;

            if (!game.CreatureManager.GetPlayerConnection(creature.CreatureId, out var connection)) continue;

            if (player.IsDead && !Equals(thing, player)) continue;

            var stackPosition = spectator.FromStackPosition;

            if (thing is IPlayer { IsDead: false } or IMonster { IsSummon: true })
                connection.OutgoingPackets.Enqueue(new MagicEffectPacket(tile.Location, EffectT.Puff));

            connection.OutgoingPackets.Enqueue(new RemoveTileThingPacket(tile, stackPosition));

            connection.Send();
        }
    }
}