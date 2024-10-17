using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location.Structs;
using Server.Contracts.Contracts.Network;

namespace Networking.Packets.Outgoing.Item;

public class RemoveTileItemPacket : OutgoingPacket
{
    public readonly IItem item;
    public readonly Location location;
    public readonly byte stackPosition;

    public RemoveTileItemPacket(Location location, byte stackPosition, IItem item)
    {
        if (item.IsNull()) return;

        this.location = location;
        this.stackPosition = stackPosition;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.AddAtStackPos);
        message.AddLocation(location);
        message.AddByte(stackPosition);
        message.AddItem(item);
    }
}