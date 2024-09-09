using System;
using System.Collections.Generic;
using Server.Entities.Models.Contracts;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items;

public delegate void CreateItem(IItem item);

public interface IItemFactory : IFactory
{
    IItem Create(ushort typeId, Location.Structs.Location location,
        IDictionary<ItemAttribute, IConvertible> attributes, IEnumerable<IItem> children = null);

    IItem Create(string name, Location.Structs.Location location, IDictionary<ItemAttribute, IConvertible> attributes,
        IEnumerable<IItem> children = null);

    IEnumerable<ICoin> CreateCoins(ulong amount);
    IItem CreateLootCorpse(ushort typeId, Location.Structs.Location location, ILoot loot);

    IItem Create(IItemType itemType, Location.Structs.Location location,
        IDictionary<ItemAttribute, IConvertible> attributes, IEnumerable<IItem> children = null);
}