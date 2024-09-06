using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;

namespace Data.InMemory;

public class ItemTypeStore : DataStore<ItemTypeStore, ushort, IItemType>, IItemTypeStore
{
}