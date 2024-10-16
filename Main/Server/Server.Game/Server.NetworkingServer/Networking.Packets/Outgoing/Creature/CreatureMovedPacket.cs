using Server.Entities.Common.Location.Structs;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class CreatureMovedPacket : OutgoingPacket
{
    private readonly Location location;
    private readonly byte stackPosition;
    private readonly Location toLocation;

    public CreatureMovedPacket(Location location, Location toLocation, byte stackPosition)
    {
        this.location = location;
        this.toLocation = toLocation;
        this.stackPosition = stackPosition;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.CreatureMoved);

        message.AddLocation(location);
        message.AddByte(stackPosition);
        message.AddLocation(toLocation);
    }
}