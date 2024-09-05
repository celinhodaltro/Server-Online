using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.DataStores;

public interface IItemTypeStore : IDataStore<ushort, IItemType>
{
}