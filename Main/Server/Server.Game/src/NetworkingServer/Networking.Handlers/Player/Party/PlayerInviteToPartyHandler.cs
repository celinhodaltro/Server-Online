﻿using Server.Entities.Common.Contracts.Services;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player.Party;

public class PlayerInviteToPartyHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly IPartyInviteService _partyInviteService;

    public PlayerInviteToPartyHandler(IGameServer game, IPartyInviteService partyInviteService)
    {
        _game = game;
        _partyInviteService = partyInviteService;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var creatureId = message.GetUInt32();
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;
        if (!_game.CreatureManager.TryGetPlayer(creatureId, out var invitedPlayer) ||
            !_game.CreatureManager.IsPlayerLogged(invitedPlayer))
        {
            connection.Send(new TextMessagePacket("Invited player is not online.", TextMessageOutgoingType.Small));
            return;
        }

        _game.Dispatcher.AddEvent(new Event(() => _partyInviteService.Invite(player, invitedPlayer)));
    }
}