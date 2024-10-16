﻿using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class CreatureSayPacket : OutgoingPacket
{
    private readonly ICreature _creature;
    private readonly Location _fromLocation;
    private readonly SpeechType _talkType;
    private readonly string _textMessage;

    public CreatureSayPacket(ICreature creature, SpeechType talkType, string textMessage)
    {
        _creature = creature;
        _talkType = talkType;
        _textMessage = textMessage;
    }

    public CreatureSayPacket(Location fromLocation, SpeechType talkType, string textMessage)
    {
        _fromLocation = fromLocation;
        _talkType = talkType;
        _textMessage = textMessage;
    }

    //todo: this code is duplicated?
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte(0xAA);
        message.AddUInt32(0x00);

        message.AddString(_creature?.Name ?? string.Empty);

        //Add level only for players
        if (_creature is IPlayer player)
            message.AddUInt16(player.Level);
        else
            message.AddUInt16(0x00);

        message.AddByte((byte)_talkType);

        message.AddLocation(_creature?.Location ?? _fromLocation);

        message.AddString(_textMessage);
    }
}