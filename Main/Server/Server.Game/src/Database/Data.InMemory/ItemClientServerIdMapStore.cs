using Server.Entities.Common.Contracts;

namespace Data.InMemory;

public class ItemClientServerIdMapStore : DataStore<ItemClientServerIdMapStore, ushort, ushort>,
    IItemClientServerIdMapStore
{
}