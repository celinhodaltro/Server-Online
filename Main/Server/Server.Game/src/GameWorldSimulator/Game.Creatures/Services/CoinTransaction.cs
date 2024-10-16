﻿using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Contracts.Services;

namespace Game.Creatures.Services;

public class CoinTransaction : ICoinTransaction
{
    private readonly ICoinTypeStore _coinTypeStore;
    private readonly IItemFactory _itemFactory;

    public CoinTransaction(IItemFactory itemFactory, ICoinTypeStore coinTypeStore)
    {
        _itemFactory = itemFactory;
        _coinTypeStore = coinTypeStore;
    }

    public void AddCoins(IPlayer player, ulong amount)
    {
        if (amount == 0 || player is null) return;

        var changeCoins = _itemFactory.CreateCoins(amount).ToList();

        player.ReceivePayment(changeCoins, amount);
    }

    /// <summary>
    ///     Removes coins from player and adds change to the bag
    /// </summary>
    /// <param name="player"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool RemoveCoins(IPlayer player, ulong amount, bool useBank = false)
    {
        if (player.GetTotalMoney(_coinTypeStore) < amount) return false;

        var removedAmount = RemoveCoins(player, amount, out var change);
        if (useBank && removedAmount < amount) player.WithdrawFromBank(amount - removedAmount);

        AddCoins(player, change);
        return true;
    }

    /// <summary>
    ///     Removes coins from player but not add change
    /// </summary>
    /// <param name="player"></param>
    /// <param name="amount"></param>
    /// <param name="change"></param>
    /// <returns></returns>
    public ulong RemoveCoins(IPlayer player, ulong amount, out ulong change)
    {
        change = 0;
        if (player is null || amount == 0) return 0;

        var backpackSlot = player.Inventory?.BackpackSlot;

        ulong removedAmount = 0;

        if (backpackSlot is null) return removedAmount;

        var moneyMap = new SortedList<uint, List<ICoin>>(); //slot and item

        var containers = new Queue<IContainer>();
        containers.Enqueue(backpackSlot);

        while (containers.TryDequeue(out var container) && amount > 0)
        {
            foreach (var item in container.Items)
            {
                if (item is IContainer childContainer)
                {
                    containers.Enqueue(childContainer);
                    continue;
                }

                if (item is not ICoin coin) continue;

                if (moneyMap.TryGetValue(coin.Worth, out var coinSlots))
                {
                    coinSlots.Add(coin);
                    continue;
                }

                coinSlots = new List<ICoin> { coin };
                moneyMap.Add(coin.Worth, coinSlots);
            }

            foreach (var money in moneyMap)
            {
                if (amount == 0) break;

                foreach (var coin in money.Value)
                {
                    if (amount == 0) break;

                    if (coin.Worth < amount)
                    {
                        container.RemoveItem(coin, coin.Amount);
                        amount -= coin.Worth;
                        removedAmount += coin.Worth;
                    }
                    else if (coin.Worth > amount)
                    {
                        var worth = coin.Worth / coin.Amount;
                        var removeCount = (uint)Math.Ceiling((decimal)(coin.Worth / worth));

                        change += worth * removeCount - amount;

                        container.RemoveItem(coin, coin.Amount);

                        return removedAmount + amount;
                    }
                    else
                    {
                        container.RemoveItem(coin, coin.Amount);
                        removedAmount += coin.Worth;
                    }
                }
            }
        }

        return removedAmount;
    }
}