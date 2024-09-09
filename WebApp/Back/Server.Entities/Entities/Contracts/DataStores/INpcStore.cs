using Server.Entities.Contracts.Creatures;

namespace Server.Entities.Contracts.DataStores;

public interface INpcStore : IDataStore<string, INpcType>
{
}