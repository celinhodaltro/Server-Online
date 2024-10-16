﻿using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player.Party;

public class PlayerRevokeInvitePartyHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerRevokeInvitePartyHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var creatureId = message.GetUInt32();
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;
        if (!_game.CreatureManager.TryGetPlayer(creatureId, out var invitedPlayer) ||
            !_game.CreatureManager.IsPlayerLogged(invitedPlayer))
        {
            connection.Send(new TextMessagePacket("Revoked player is not online.", TextMessageOutgoingType.Small));
            return;
        }

        _game.Dispatcher.AddEvent(new Event(() => player.PlayerParty.RevokePartyInvite(invitedPlayer)));
    }
}