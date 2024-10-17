using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts;

public interface INpcStore : IDataStore<string, INpcType>
{
}