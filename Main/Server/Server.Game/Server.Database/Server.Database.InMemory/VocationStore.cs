using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;

namespace Data.InMemory;

public class VocationStore : DataStore<VocationStore, byte, IVocation>, IVocationStore
{
}