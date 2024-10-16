﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Party;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Party;
using Server.Common.Contracts;

namespace Server.Events.Player.Party;

public class PlayerJoinedPartyEventHandler
{
    private readonly IGameServer game;

    public PlayerJoinedPartyEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer newMember, IParty party)
    {
        if (Guard.AnyNull(newMember, party)) return;

        game.CreatureManager.GetPlayerConnection(newMember.CreatureId, out var newMemberConnection);

        newMemberConnection?.OutgoingPackets?.Enqueue(new PartyEmblemPacket(newMember, PartyEmblem.Member));
        newMemberConnection?.OutgoingPackets?.Enqueue(new TextMessagePacket(
            $"You have joined {party.Leader.Name}'s party. Open the party channel to communicate with your companions.",
            TextMessageOutgoingType.Description));

        foreach (var member in party.Members)
        {
            if (member == newMember) continue;
            if (!game.CreatureManager.GetPlayerConnection(member.CreatureId, out var memberConnection)) continue;

            memberConnection.OutgoingPackets.Enqueue(new TextMessagePacket($"{newMember.Name} has joined the party",
                TextMessageOutgoingType.Description));

            if (member.CanSee(newMember.Location))
            {
                memberConnection.OutgoingPackets.Enqueue(new PartyEmblemPacket(newMember, PartyEmblem.Member));
                newMemberConnection?.OutgoingPackets?.Enqueue(new PartyEmblemPacket(member,
                    party.IsLeader(member.CreatureId) ? PartyEmblem.Leader : PartyEmblem.Member));
            }

            memberConnection.Send();
        }

        newMemberConnection?.Send();
    }
}