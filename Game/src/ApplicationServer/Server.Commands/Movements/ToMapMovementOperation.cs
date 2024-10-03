using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Creatures.Structs;
using Server.Entities.Common.Location;
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