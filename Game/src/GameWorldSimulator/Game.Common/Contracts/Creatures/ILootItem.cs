using System;
using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.Creatures;

public interface ILootItem
{
    Func<IItemType> ItemType { get; }
    byte Amount { get; }
    uint Chance { get; }
    ILootItem[] Items { get; }
}