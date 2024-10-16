﻿using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class CreatureChangeSpeedPacket : OutgoingPacket
{
    private readonly uint _creaturedId;
    private readonly ushort _speed;

    public CreatureChangeSpeedPacket(uint creaturedId, ushort speed)
    {
        _creaturedId = creaturedId;
        _speed = speed;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.ChangeSpeed);

        message.AddUInt32(_creaturedId);
        message.AddUInt16(_speed);
    }
}