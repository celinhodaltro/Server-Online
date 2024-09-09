using System;
using System.Collections.Generic;
using Game.Common.Location.Structs;
using Server.Entities.Models.Contracts;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items;

public delegate void CreateItem(IItem item);

public interface IItemFactory : IFactory
{
    IItem Create(ushort typeId, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes, IEnumerable<IItem> children = null);

    IItem Create(string name, Location location, IDictionary<ItemAttribute, IConvertible> attributes,
        IEnumerable<IItem> children = null);

    IEnumerable<ICoin> CreateCoins(ulong amount);
    IItem CreateLootCorpse(ushort typeId, Location location, ILoot loot);

    IItem Create(IItemType itemType, Location location,
        IDictionary<ItemAttribute, IConvertible> attributes, IEnumerable<IItem> children = null);
}