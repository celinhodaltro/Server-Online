using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Location;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class CreatureMovedDownPacket : OutgoingPacket
{
    private readonly ICreature _creature;
    private readonly Location _fromLocation;
    private readonly IMap _map;
    private readonly Location _toLocation;

    public CreatureMovedDownPacket(Location fromLocation, Location toLocation, IMap map, ICreature creature)
    {
        _fromLocation = fromLocation;
        _toLocation = toLocation;
        _map = map;
        _creature = creature;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.FloorChangeDown);

        var skip = -1;
        var x = (ushort)(_fromLocation.X - MapViewPort.MaxClientViewPortX);
        var y = (ushort)(_fromLocation.Y - MapViewPort.MaxClientViewPortY);

        //going from surface to underground
        if (_toLocation.Z == 8)
            for (var i = 0; i < 3; ++i)
                message.AddBytes(_map
                    .GetFloorDescription(_creature, x, y, (byte)(_toLocation.Z + i), 18, 14, -i - 1, ref skip)
                    .ToArray());

        //going further down
        if (_toLocation.Z > _fromLocation.Z && _toLocation.Z is > 8 and < 14)
        {
            skip = -1;
            message.AddBytes(_map
                .GetFloorDescription(_creature, x, y, (byte)(_toLocation.Z + 2), 18, 14, -3, ref skip).ToArray());
        }

        if (skip >= 0)
        {
            message.AddByte((byte)skip);
            message.AddByte(0xFF);
        }

        //east
        message.AddByte((byte)GameOutgoingPacketType.MapSliceEast);
        message.AddBytes(_map.GetDescription(_creature, (ushort)(_fromLocation.X + MapViewPort.MaxClientViewPortX + 1),
            (ushort)(y - 1), _toLocation.Z, 1).ToArray());

        //south
        message.AddByte((byte)GameOutgoingPacketType.MapSliceSouth);
        message.AddBytes(_map.GetDescription(_creature, x,
            (ushort)(_fromLocation.Y + MapViewPort.MaxClientViewPortY + 1), _toLocation.Z, 18, 1).ToArray());
    }
}