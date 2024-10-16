﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Results;

namespace Game.Items.Services.ItemTransform.Operations;

internal static class ReplaceItemOnInventoryOperation
{
    public static Result<IItem> Execute(IItemFactory itemFactory, IItem fromItem, IItemType toItemType)
    {
        if (fromItem.Owner is not IPlayer player) return Result<IItem>.NotApplicable;

        var updated = player.Inventory.UpdateItem(fromItem, toItemType);
        if (updated) return Result<IItem>.Ok(fromItem);

        var result = player.Inventory.RemoveItem(fromItem.Metadata.BodyPosition, fromItem.Amount);
        if (result.Failed) return Result<IItem>.Fail(result.Error);

        if (toItemType is null) return Result<IItem>.Ok(null);

        var createdItem = itemFactory.Create(toItemType, fromItem.Location, null);

        if (createdItem is null) return Result<IItem>.Ok(null);

        var addItemResult = player.Inventory.AddItem(createdItem, fromItem.Metadata.BodyPosition);

        return addItemResult.Succeeded ? Result<IItem>.Ok(createdItem) : Result<IItem>.Fail(addItemResult.Error);
    }
}