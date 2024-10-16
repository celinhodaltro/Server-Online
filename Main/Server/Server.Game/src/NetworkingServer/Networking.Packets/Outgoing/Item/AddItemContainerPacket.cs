﻿using Server.Entities.Common.Contracts.Items;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Item;

public class AddItemContainerPacket : OutgoingPacket
{
    private readonly byte containerId;
    private readonly IItem item;

    public AddItemContainerPacket(byte containerId, IItem item)
    {
        this.containerId = containerId;
        this.item = item;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.ContainerAddItem);

        message.AddByte(containerId);
        message.AddItem(item);
    }
}