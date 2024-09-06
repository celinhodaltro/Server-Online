using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;

namespace Data.InMemory;

public class CoinTypeStore : DataStore<CoinTypeStore, ushort, IItemType>, ICoinTypeStore
{
}