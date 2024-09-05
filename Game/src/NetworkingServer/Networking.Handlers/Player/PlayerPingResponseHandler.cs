using System;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Player;

public sealed class PlayerPingResponseHandler : PacketHandler
{
    private readonly IGameServer _game;

    public PlayerPingResponseHandler(IGameServer game)
    {
        _game = game;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        _game.Dispatcher.AddEvent(new Event(() => connection.LastPingResponse = DateTime.Now.Ticks));
    }
}