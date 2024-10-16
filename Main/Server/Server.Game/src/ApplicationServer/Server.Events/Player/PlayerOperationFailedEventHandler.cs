﻿using Server.Entities.Common;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerOperationFailedEventHandler
{
    private readonly IGameServer game;

    public PlayerOperationFailedEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(uint playerId, string message)
    {
        if (!game.CreatureManager.GetPlayerConnection(playerId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(message,
            TextMessageOutgoingType.MESSAGE_STATUS_DEFAULT));
        connection.Send();
    }

    public void Execute(uint playerId, InvalidOperation invalidOperation)
    {
        if (!game.CreatureManager.GetPlayerConnection(playerId, out var connection)) return;

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(TextMessageOutgoingParser.Parse(invalidOperation),
            TextMessageOutgoingType.MESSAGE_STATUS_DEFAULT));
        connection.Send();
    }
}