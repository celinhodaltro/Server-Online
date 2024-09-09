using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.DataStores;

public interface ICoinTypeStore : IDataStore<ushort, IItemType>
{
}