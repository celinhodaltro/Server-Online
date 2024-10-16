using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Party;
using Server.Entities.Common.Helpers;
using Networking.Packets.Outgoing.Party;
using Server.Contracts.Contracts;

namespace Server.Events.Player.Party;

public class PlayerInviteToPartyEventHandler
{
    private readonly IGameServer game;

    public PlayerInviteToPartyEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer leader, IPlayer invited, IParty party)
    {
        if (Guard.AnyNull(leader, invited, party)) return;

        if (!game.CreatureManager.GetPlayerConnection(leader.CreatureId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new PartyEmblemPacket(leader, PartyEmblem.Leader));
        connection.Send();
    }
}