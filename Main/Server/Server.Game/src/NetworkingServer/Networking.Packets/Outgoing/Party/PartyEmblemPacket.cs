﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Party;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Party;

public class PartyEmblemPacket : OutgoingPacket
{
    private readonly ICreature creature;
    private readonly PartyEmblem emblem;

    public PartyEmblemPacket(ICreature creature, PartyEmblem emblem)
    {
        this.creature = creature;
        this.emblem = emblem;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.CreatureEmblem);
        message.AddUInt32(creature.CreatureId);
        message.AddByte((byte)emblem);
    }
}