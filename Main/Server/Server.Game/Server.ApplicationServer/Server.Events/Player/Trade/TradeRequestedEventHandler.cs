using Server.Entities.Common.Helpers;
using Game.Systems.SafeTrade;
using Game.Systems.SafeTrade.Request;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Trade;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;

namespace Server.Events.Player.Trade;

public class TradeRequestedEventHandler : IEventHandler
{
    private readonly IGameServer _gameServer;

    public TradeRequestedEventHandler(IGameServer gameServer)
    {
        _gameServer = gameServer;
    }

    public void Execute(TradeRequest tradeRequest)
    {
        if (Guard.AnyNull(tradeRequest, tradeRequest.PlayerRequesting, tradeRequest.PlayerRequested)) return;

        _gameServer.CreatureManager.GetPlayerConnection(tradeRequest.PlayerRequesting.CreatureId,
            out var playerRequestingConnection);
        _gameServer.CreatureManager.GetPlayerConnection(tradeRequest.PlayerRequested.CreatureId,
            out var playerRequestedConnection);

        playerRequestingConnection.OutgoingPackets.Enqueue(new TradeRequestPacket(tradeRequest.PlayerRequesting.Name,
            tradeRequest.Items));

        SendTradeMessage(tradeRequest, playerRequestedConnection);

        SendAcknowledgeTradeToBothPlayers(tradeRequest, playerRequestingConnection, playerRequestedConnection);

        playerRequestingConnection.Send();
        playerRequestedConnection.Send();
    }

    private static void SendTradeMessage(TradeRequest tradeRequest, IConnection playerRequestedConnection)
    {
        if (tradeRequest.PlayerAcknowledgedTrade) return;

        var message = $"{tradeRequest.PlayerRequested.Name} wants to trade with you.";
        playerRequestedConnection.OutgoingPackets.Enqueue(new TextMessagePacket(message,
            TextMessageOutgoingType.Small));
    }

    private static void SendAcknowledgeTradeToBothPlayers(TradeRequest tradeRequest,
        IConnection playerRequestingConnection,
        IConnection playerRequestedConnection)
    {
        if (!tradeRequest.PlayerAcknowledgedTrade) return;

        var items = SafeTradeSystem.GetTradedItems(tradeRequest.PlayerRequested);

        playerRequestingConnection.OutgoingPackets.Enqueue(new TradeRequestPacket(tradeRequest.PlayerRequested.Name,
            items, true));

        playerRequestedConnection.OutgoingPackets.Enqueue(new TradeRequestPacket(tradeRequest.PlayerRequesting.Name,
            tradeRequest.Items, true));
    }
}