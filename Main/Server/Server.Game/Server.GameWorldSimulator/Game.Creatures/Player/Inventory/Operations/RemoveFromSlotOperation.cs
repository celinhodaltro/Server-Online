﻿using Server.Entities.Common;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Results;

namespace Game.Creatures.Player.Inventory.Operations;

internal static class RemoveFromSlotOperation
{
    internal static Result<IItem> Remove(Inventory inventory, Slot slot, byte amount)
    {
        if (amount == 0) return Result<IItem>.Fail(InvalidOperation.Impossible);

        if (inventory.InventoryMap.GetItem<IItem>(slot) is not { } item)
            return Result<IItem>.Fail(InvalidOperation.Impossible);

        var removedItem = GetRemovedItem(inventory, slot, amount, item);

        if (removedItem is IDressable dressable) dressable.UndressFrom(inventory.Owner);

        if (removedItem is ICumulative cumulative) cumulative.ClearSubscribers();
        if (removedItem is IContainer container)
            container.UnsubscribeFromWeightChangeEvent(inventory.ContainerOnOnWeightChanged);

        removedItem.OnItemRemoved(inventory.Owner);

        return Result<IItem>.Ok(removedItem);
    }

    private static IItem GetRemovedItem(Inventory inventory, Slot slot, byte amount, IItem item)
    {
        if (item is ICumulative cumulative && amount < cumulative.Amount) return cumulative.Split(amount);

        if (item is ICumulative c) c.ClearSubscribers();

        inventory.InventoryMap.Remove(slot);
        return item;
    }
}