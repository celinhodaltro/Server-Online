using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.DataStores;

public interface INpcStore : IDataStore<string, INpcType>
{
}