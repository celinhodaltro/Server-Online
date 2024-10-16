using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;

namespace Data.InMemory;

public class NpcStore : DataStore<NpcStore, string, INpcType>, INpcStore
{
}