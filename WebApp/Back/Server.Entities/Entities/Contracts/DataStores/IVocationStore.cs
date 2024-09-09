using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.DataStores;

public interface IVocationStore : IDataStore<byte, IVocation>, IDataStore
{
}