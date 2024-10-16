﻿using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming;

public abstract class IncomingPacket
{
    public uint[] Xtea { get; } = new uint[4];

    protected void LoadXtea(IReadOnlyNetworkMessage message)
    {
        for (var i = 0; i < 4; i++) Xtea[i] = message.GetUInt32();
    }
}