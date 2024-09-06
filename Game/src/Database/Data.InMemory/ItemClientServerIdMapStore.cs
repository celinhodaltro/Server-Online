using Game.Common.Contracts.DataStores;

namespace Data.InMemory;

public class ItemClientServerIdMapStore : DataStore<ItemClientServerIdMapStore, ushort, ushort>,
    IItemClientServerIdMapStore
{
}