using System;
using Data.Interfaces;
using Networking.Packets.Incoming.Chat;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Chat;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Networking.Handlers.Chat;

public class PlayerOpenPrivateChannelHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly IPlayerRepository _playerRepository;

    public PlayerOpenPrivateChannelHandler(IGameServer game, IPlayerRepository playerRepository)
    {
        _game = game;
        _playerRepository = playerRepository;
    }

    public override async void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var channel = new OpenPrivateChannelPacket(message);
        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        if (string.IsNullOrWhiteSpace(channel.Receiver) ||
            await _playerRepository.GetPlayer(channel.Receiver) is null)
        {
            connection.Send(new TextMessagePacket("A player with this name does not exist.",
                TextMessageOutgoingType.Small));
            return;
        }

        if (string.IsNullOrWhiteSpace(channel.Receiver) ||
            !_game.CreatureManager.TryGetPlayer(channel.Receiver, out var receiver))
        {
            connection.Send(new TextMessagePacket("A player with this name is not online.",
                TextMessageOutgoingType.Small));
            return;
        }

        if (channel.Receiver.Trim().Equals(player.Name.Trim(), StringComparison.InvariantCultureIgnoreCase))
        {
            connection.Send(new TextMessagePacket("You cannot set up a private message channel with yourself.",
                TextMessageOutgoingType.Small));
            return;
        }

        if (receiver is null) return;

        connection.OutgoingPackets.Enqueue(new PlayerOpenPrivateChannelPacket(receiver.Name));
        connection.Send();
    }
}