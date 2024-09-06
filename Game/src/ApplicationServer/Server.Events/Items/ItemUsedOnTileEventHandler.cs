using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types.Usable;
using Game.Common.Contracts.World.Tiles;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;

namespace Server.Events.Items;

public class ItemUsedOnTileEventHandler
{
    private readonly IGameServer game;

    public ItemUsedOnTileEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature usedBy, ITile onTile, IUsableOnTile item)
    {
        foreach (var spectator in game.Map.GetPlayersAtPositionZone(usedBy.Location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            if (item.Metadata.ShootType != default)
                connection.OutgoingPackets.Enqueue(new DistanceEffectPacket(usedBy.Location, onTile.Location,
                    (byte)item.Metadata.ShootType));
            connection.Send();
        }
    }
}