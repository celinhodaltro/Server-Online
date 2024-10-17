﻿using System;
using System.Collections.Generic;
using System.Linq;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Results;
using Game.Items.Items.Containers.Container.Operations.Update;
using Game.Items.Items.Containers.Container.Queries;
using Game.Items.Items.Containers.Container.Rules;

namespace Game.Items.Items.Containers.Container.Operations.Add;

internal static class AddItemOperation
{
    public static Result TryAddItem(Container toContainer, IItem item, byte? slot = null)
    {
        if (item is null) return Result.NotPossible;

        if (slot.HasValue && toContainer.Capacity <= slot) slot = null;

        var validation = CanAddItemToContainerRule.CanAdd(toContainer, item, slot);
        if (!validation.Succeeded) return validation;

        slot ??= toContainer.LastFreeSlot;

        return AddItem(toContainer, item, slot);
    }

    public static void AddChildren(Container container, IEnumerable<IItem> children)
    {
        if (children is null) return;

        foreach (var item in children.Reverse()) TryAddItem(container, item);
    }

    private static Result AddItem(Container toContainer, IItem item, byte? slot)
    {
        var result = toContainer.GetContainerAt(slot.Value, out var container)
            ? container.AddItem(item).ResultValue
            : AddItem(toContainer, item, slot.Value);

        item.SetParent(container ?? toContainer);

        return result;
    }

    private static Result AddItem(Container toContainer, IItem item, byte slot)
    {
        if (item is null) return Result.NotPossible;
        if (toContainer.Capacity <= slot) throw new ArgumentOutOfRangeException(nameof(toContainer));

        if (item is not ICumulative cumulativeItem) return AddItemToFrontOperation.Add(toContainer, item);

        var itemToJoinSlot = FindSlotOfFirstItemNotFullyQuery.Find(toContainer, cumulativeItem);

        if (itemToJoinSlot >= 0 && cumulativeItem is { } cumulative)
            return JoinCumulativeItemOperation.Join(toContainer, cumulative, (byte)itemToJoinSlot);

        return AddItemToFrontOperation.Add(toContainer, cumulativeItem);
    }
}