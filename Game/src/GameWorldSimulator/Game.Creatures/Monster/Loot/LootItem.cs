using System;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;

namespace Game.Creatures.Monster.Loot;

public record LootItem(Func<IItemType> ItemType, byte Amount, uint Chance, ILootItem[] Items) : ILootItem;