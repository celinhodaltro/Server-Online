using System;
using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Creatures;

public interface ILootItem
{
    Func<IItemType> ItemType { get; }
    byte Amount { get; }
    uint Chance { get; }
    ILootItem[] Items { get; }
}