using System;
using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.Creatures;

public interface ILootItem
{
    Func<IItemType> ItemType { get; }
    byte Amount { get; }
    uint Chance { get; }
    ILootItem[] Items { get; }
}