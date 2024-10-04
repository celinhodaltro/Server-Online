using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;

namespace Data.InMemory;

public class CoinTypeStore : DataStore<CoinTypeStore, ushort, IItemType>, ICoinTypeStore
{
}