using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.DataStores;

public interface ICoinTypeStore : IDataStore<ushort, IItemType>
{
}