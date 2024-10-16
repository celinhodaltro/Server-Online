﻿using System;
using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Services;
using Server.Common.Contracts;

namespace Server.Managers;

public class DecayableItemManager : IDecayableItemManager
{
    private readonly IDecayService _decayService;
    private readonly PriorityQueue<IItem, DateTime> _items;

    public DecayableItemManager(IDecayService decayService)
    {
        _decayService = decayService;
        _items = new PriorityQueue<IItem, DateTime>();
    }

    public void Add(IItem item)
    {
        if (item.Decay is null) return;

        var expiresAt = DateTime.Now.AddSeconds(item.Decay.Remaining);
        _items.Enqueue(item, expiresAt);
    }

    public void DecayExpiredItems()
    {
        while (_items.Count > 0)
        {
            var item = _items.Peek();

            if (!item.IsDeleted && !item.Decay.IsPaused && !item.Decay.Expired) break;

            if (item.Decay.Expired) _decayService.Decay(item);

            _items.Dequeue();
        }
    }
}