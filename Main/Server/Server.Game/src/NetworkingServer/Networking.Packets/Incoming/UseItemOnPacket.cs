﻿using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming;

public class UseItemOnPacket : IncomingPacket
{
    public UseItemOnPacket(IReadOnlyNetworkMessage message)
    {
        Location = message.GetLocation();
        ClientId = message.GetUInt16();
        StackPosition = message.GetByte();
        ToLocation = message.GetLocation();
        ToClientId = message.GetUInt16();
        ToStackPosition = message.GetByte();
    }

    public Location Location { get; }
    public ushort ClientId { get; }
    public byte StackPosition { get; set; }
    public Location ToLocation { get; }
    public ushort ToClientId { get; }
    public byte ToStackPosition { get; set; }
}