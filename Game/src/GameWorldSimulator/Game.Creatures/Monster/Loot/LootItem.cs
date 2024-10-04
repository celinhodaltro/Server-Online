using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;

namespace Game.Creatures.Monster.Loot;

public record LootItem(Func<IItemType> ItemType, byte Amount, uint Chance, ILootItem[] Items) : ILootItem;