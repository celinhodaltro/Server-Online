using Game.Common.Contracts.Items;
using Game.Common.Contracts.World;
using Game.Common.Helpers;
using Networking.Packets.Outgoing.Item;
using Server.Common.Contracts;

namespace Server.Events.Tiles;

public class ThingUpdatedOnTileEventHandler
{
    private readonly IGameServer game;

    public ThingUpdatedOnTileEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IThing thing, ICylinder cylinder)
    {
        if (Guard.AnyNull(cylinder, cylinder.TileSpectators, thing)) return;

        var tile = cylinder.ToTile;
        if (tile.IsNull()) return;

        foreach (var spectator in cylinder.TileSpectators)
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.Spectator.CreatureId, out var connection))
                continue;

            if (!spectator.Spectator.CanSee(thing.Location)) continue;

            connection.OutgoingPackets.Enqueue(new UpdateTileItemPacket(thing.Location, spectator.ToStackPosition,
                (IItem)thing));

            connection.Send();
        }
    }
}