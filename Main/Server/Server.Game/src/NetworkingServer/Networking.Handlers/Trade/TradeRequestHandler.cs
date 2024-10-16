using Networking.Packets.Incoming.Trade;
using Server.Commands.Trade;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;

namespace Networking.Handlers.Trade;

public class TradeRequestHandler : PacketHandler
{
    private readonly IGameServer _gameServer;
    private readonly TradeRequestCommand _tradeRequestCommand;

    public TradeRequestHandler(IGameServer gameServer, TradeRequestCommand tradeRequestCommand)
    {
        _gameServer = gameServer;
        _tradeRequestCommand = tradeRequestCommand;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        var tradeRequestPacket = new TradeRequestPacket(message);
        if (!_gameServer.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        if (player is null) return;

        _gameServer.Dispatcher.AddEvent(new Event(2000,
            () => _tradeRequestCommand.RequestTrade(player, tradeRequestPacket)));
    }
}