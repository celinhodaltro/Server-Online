using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.DataStores;

public interface INpcStore : IDataStore<string, INpcType>
{
}