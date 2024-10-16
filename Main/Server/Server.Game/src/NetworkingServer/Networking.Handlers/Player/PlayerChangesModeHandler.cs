﻿using Networking.Packets.Incoming;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player;

public class PlayerChangesModeHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerChangesModeHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var changeMode = new ChangeModePacket(message);

        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        _game.Dispatcher.AddEvent(new Event(() =>
        {
            player.ChangeFightMode(changeMode.FightMode);
            player.ChangeChaseMode(changeMode.ChaseMode);
            player.ChangeSecureMode(changeMode.SecureMode);
        }));
    }
}