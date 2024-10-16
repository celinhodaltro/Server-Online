﻿using Networking.Packets.Incoming;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Chat;

public class PlayerSayHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly PlayerSayCommand _playerSayCommand;

    public PlayerSayHandler(IGameServer game, PlayerSayCommand playerSayCommand)
    {
        _game = game;
        _playerSayCommand = playerSayCommand;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var playerSay = new PlayerSayPacket(message);
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        _game.Dispatcher.AddEvent(new Event(() => _playerSayCommand.Execute(player, connection, playerSay)));
    }
}