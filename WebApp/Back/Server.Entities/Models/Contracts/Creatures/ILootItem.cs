using System;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Creatures;

public interface ILootItem
{
    Func<IItemType> ItemType { get; }
    byte Amount { get; }
    uint Chance { get; }
    ILootItem[] Items { get; }
}