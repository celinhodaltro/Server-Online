﻿using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Runes;
using Server.Entities.Common.Contracts.Items.Types.Usable;

namespace Game.Items.Events;

public class ItemEventSubscriber : IItemEventSubscriber, IGameEventSubscriber
{
    private readonly FieldRuneUsedEventHandler _fieldRuneUsedEventHandler;
    private readonly ItemUsedEventHandler _itemUsedEventHandler;

    public ItemEventSubscriber(ItemUsedEventHandler itemUsedEventHandler,
        FieldRuneUsedEventHandler fieldRuneUsedEventHandler)
    {
        _itemUsedEventHandler = itemUsedEventHandler;
        _fieldRuneUsedEventHandler = fieldRuneUsedEventHandler;
    }

    public void Subscribe(IItem item)
    {
        if (item is null) return;

        if (item is IConsumable consumable) consumable.OnUsed += _itemUsedEventHandler.Execute;
        if (item is IFieldRune fieldRune) fieldRune.OnUsedOnTile += _fieldRuneUsedEventHandler.Execute;
    }

    public void Unsubscribe(IItem item)
    {
        if (item is null) return;

        if (item is IConsumable consumable) consumable.OnUsed -= _itemUsedEventHandler.Execute;
        if (item is IFieldRune fieldRune) fieldRune.OnUsedOnTile -= _fieldRuneUsedEventHandler.Execute;
    }
}