﻿using System.Collections.Generic;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Body;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.Creatures;

public delegate void RemoveItemFromSlot(IInventory inventory, IItem item, Slot slot, byte amount = 1);

public delegate void AddItemToSlot(IInventory inventory, IItem item, Slot slot, byte amount = 1);

public delegate void FailAddItemToSlot(IPlayer player, InvalidOperation invalidOperation);

public delegate void ChangeInventoryWeight(IInventory inventory);

public interface IInventory : IHasItem
{
    IPlayer Owner { get; }
    ushort TotalAttack { get; }

    ushort TotalDefense { get; }

    ushort TotalArmor { get; }

    byte AttackRange { get; }
    IContainer BackpackSlot { get; }
    IWeapon Weapon { get; }
    bool HasShield { get; }
    float TotalWeight { get; }
    IDictionary<ushort, uint> Map { get; }
    IEnumerable<IItem> DressingItems { get; }
    bool IsUsingWeapon { get; }
    IItem this[Slot slot] { get; }
    ulong GetTotalMoney(ICoinTypeStore coinTypeStore);
    Result<IItem> RemoveItem(Slot slot, byte amount);
    T TryGetItem<T>(Slot slot);
    Result<OperationResultList<IItem>> AddItem(IItem item, Slot slot = Slot.None);

    bool UpdateItem(IItem item, IItemType newType);

    #region Events

    event AddItemToSlot OnItemAddedToSlot;
    event FailAddItemToSlot OnFailedToAddToSlot;
    event RemoveItemFromSlot OnItemRemovedFromSlot;
    event ChangeInventoryWeight OnWeightChanged;

    #endregion
}