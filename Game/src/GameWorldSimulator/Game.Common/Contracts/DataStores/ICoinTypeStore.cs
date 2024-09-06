using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.DataStores;

public interface ICoinTypeStore : IDataStore<ushort, IItemType>
{
}