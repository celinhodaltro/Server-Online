using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;

namespace Data.InMemory;

public class NpcStore : DataStore<NpcStore, string, INpcType>, INpcStore
{
}