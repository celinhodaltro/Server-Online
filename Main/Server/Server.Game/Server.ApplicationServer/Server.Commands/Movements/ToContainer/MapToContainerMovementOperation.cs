using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Networking.Packets.Incoming;
using Server.Contracts.Contracts;

namespace Server.Commands.Movements.ToContainer;

public class MapToContainerMovementOperation
{
    private readonly IItemMovementService _itemMovementService;

    public MapToContainerMovementOperation(IItemMovementService itemMovementService)
    {
        _itemMovementService = itemMovementService;
    }

    public void Execute(IPlayer player, IGameServer game, IMap map, ItemThrowPacket itemThrow)
    {
        MapToContainer(player, map, itemThrow);
    }

    private void MapToContainer(IPlayer player, IMap map, ItemThrowPacket itemThrow)
    {
        var tile = map[itemThrow.FromLocation];

        if (tile is not IDynamicTile fromTile) return;
        var item = fromTile.TopItemOnStack;

        if (item is null) return;
        if (!item.IsPickupable) return;

        var container = player.Containers[itemThrow.ToLocation.ContainerId];
        if (container is null) return;

        _itemMovementService.Move(player, item, fromTile, container,
            itemThrow.Count, 0, (byte)itemThrow.ToLocation.ContainerSlot);
    }

    public static bool IsApplicable(ItemThrowPacket itemThrowPacket)
    {
        return itemThrowPacket.FromLocation.Type == LocationType.Ground
               && itemThrowPacket.ToLocation.Type == LocationType.Container;
    }
}