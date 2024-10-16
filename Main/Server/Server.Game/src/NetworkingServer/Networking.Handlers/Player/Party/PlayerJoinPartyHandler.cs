﻿using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player.Party;

public class PlayerJoinPartyHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerJoinPartyHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var leaderId = message.GetUInt32();
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;
        if (!_game.CreatureManager.TryGetPlayer(leaderId, out var leader) ||
            !_game.CreatureManager.IsPlayerLogged(leader))
        {
            connection.Send(new TextMessagePacket("Player is not online.", TextMessageOutgoingType.Small));
            return;
        }

        _game.Dispatcher.AddEvent(new Event(() => player.PlayerParty.JoinParty(leader.PlayerParty.Party)));
    }
}