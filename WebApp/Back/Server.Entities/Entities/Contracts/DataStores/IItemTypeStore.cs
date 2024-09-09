using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.DataStores;

public interface IItemTypeStore : IDataStore<ushort, IItemType>
{
}