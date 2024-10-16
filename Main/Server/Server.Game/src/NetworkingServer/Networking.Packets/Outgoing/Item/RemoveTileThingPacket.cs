﻿using Server.Entities.Common.Contracts.World.Tiles;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Item;

public class RemoveTileThingPacket : OutgoingPacket
{
    private readonly byte stackPosition;
    private readonly ITile tile;

    public RemoveTileThingPacket(ITile tile, byte stackPosition)
    {
        this.tile = tile;
        this.stackPosition = stackPosition;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.RemoveAtStackPos);
        message.AddLocation(tile.Location);
        message.AddByte(stackPosition);
    }
}