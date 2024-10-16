﻿using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Item;

public class UpdateTileItemPacket : OutgoingPacket
{
    public readonly IItem item;
    public readonly Location location;
    public readonly byte stackPosition;

    public UpdateTileItemPacket(Location location, byte stackPosition, IItem item)
    {
        if (item.IsNull()) return;

        this.location = location;
        this.stackPosition = stackPosition;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.TransformThing);
        message.AddLocation(location);
        message.AddByte(stackPosition);
        message.AddItem(item);
    }
}