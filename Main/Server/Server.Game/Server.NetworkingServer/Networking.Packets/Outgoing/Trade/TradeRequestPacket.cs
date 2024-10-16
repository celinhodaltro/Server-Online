﻿using Server.Entities.Common.Contracts.Items;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Trade;

public class TradeRequestPacket : IOutgoingPacket
{
    public TradeRequestPacket(string playerName, IItem[] items, bool acknowledged = false)
    {
        PlayerName = playerName;
        Items = items;
        Acknowledged = acknowledged;
    }

    private string PlayerName { get; }
    private IItem[] Items { get; }
    private bool Acknowledged { get; }

    public void WriteToMessage(INetworkMessage message)
    {
        message.AddByte(Acknowledged
            ? (byte)GameOutgoingPacketType.AcknowlegdeTradeRequest
            : (byte)GameOutgoingPacketType.TradeRequest);

        message.AddString(PlayerName);

        message.AddByte((byte)Items.Length);
        foreach (var item in Items) message.AddItem(item);
    }
}