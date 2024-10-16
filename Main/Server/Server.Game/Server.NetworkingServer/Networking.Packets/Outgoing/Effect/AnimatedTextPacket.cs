﻿using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Texts;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Effect;

public class AnimatedTextPacket : OutgoingPacket
{
    private readonly TextColor color;
    private readonly Location location;
    private readonly string text;

    public AnimatedTextPacket(Location location, TextColor color, string message)
    {
        this.location = location;
        this.color = color;
        text = message;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.AnimatedText);
        message.AddLocation(location);
        message.AddByte((byte)color);
        message.AddString(text);
    }
}