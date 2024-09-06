using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Services;
using Game.Common.Creatures.Structs;
using Game.Common.Location;
using Networking.Packets.Incoming;

namespace Server.Commands.Movements;

public class ToMapMovementOperation
{
    public static void Execute(IPlayer player, ItemThrowPacket itemThrow, IToMapMovementService toMapMovementService)
    {
        var movementParams = new MovementParams(itemThrow.FromLocation, itemThrow.ToLocation, itemThrow.Count);
        toMapMovementService.Move(player, movementParams);
    }

    public static bool IsApplicable(ItemThrowPacket itemThrowPacket)
    {
        return itemThrowPacket.ToLocation.Type == LocationType.Ground;
    }
}