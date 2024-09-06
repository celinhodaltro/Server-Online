using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;

namespace Data.InMemory;

public class VocationStore : DataStore<VocationStore, byte, IVocation>, IVocationStore
{
}