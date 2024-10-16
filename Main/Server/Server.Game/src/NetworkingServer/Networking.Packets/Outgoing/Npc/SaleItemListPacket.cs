﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Npc;

public class SaleItemListPacket : OutgoingPacket
{
    private readonly ICoinTypeStore _coinTypeStore;

    public SaleItemListPacket(IPlayer player, IEnumerable<IShopItem> shopItem, ICoinTypeStore coinTypeStore)
    {
        _coinTypeStore = coinTypeStore;
        Player = player;
        ShopItems = shopItem;
    }

    public IPlayer Player { get; }
    public IEnumerable<IShopItem> ShopItems { get; }

    public override void WriteToMessage(INetworkMessage message)
    {
        if (Player is null || ShopItems is null) return;

        var map = Player.Inventory.Map;
        var totalMoney = Player.Inventory.GetTotalMoney(_coinTypeStore) + Player.BankAmount;

        message.AddByte((byte)GameOutgoingPacketType.SaleItemList);
        message.AddUInt32((uint)Math.Min(totalMoney, uint.MaxValue));

        byte itemsToSend = 0;

        var temp = new List<byte>();
        foreach (var shopItem in ShopItems)
        {
            if (shopItem.SellPrice == 0) continue;

            var index = (int)shopItem.Item.TypeId;
            //if (Item::items[shopInfo.itemId].isFluidContainer()) //todo
            //{
            //    index |= (static_cast<uint32_t>(shopInfo.subType) << 16);
            //}

            if (!map.TryGetValue(shopItem.Item.TypeId, out var itemAmount)) continue;

            temp.AddRange(BitConverter.GetBytes(shopItem.Item.ClientId));
            temp.Add((byte)Math.Min(itemAmount, byte.MaxValue));

            if (++itemsToSend >= byte.MaxValue) break;
        }

        message.AddByte(itemsToSend);
        message.AddBytes(temp.ToArray());
    }
}