﻿using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Player;

public class CancelTargetPacket : OutgoingPacket
{
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.CancelTarget);
        message.AddUInt32(0x00);
    }
}