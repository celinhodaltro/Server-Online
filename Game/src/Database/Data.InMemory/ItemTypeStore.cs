using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;

namespace Data.InMemory;

public class ItemTypeStore : DataStore<ItemTypeStore, ushort, IItemType>, IItemTypeStore
{
}