﻿using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Chat;

public class PlayerUpdateVipStatusPacket : OutgoingPacket
{
    private readonly bool online;
    private readonly uint playerId;

    public PlayerUpdateVipStatusPacket(uint playerId, bool online)
    {
        this.playerId = playerId;
        this.online = online;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)(online
            ? GameOutgoingPacketType.OnlineStatusVip
            : GameOutgoingPacketType.OfflineStatusVip));
        message.AddUInt32(playerId);
    }
}