using Server.Entities.Contracts.Creatures;

namespace Server.Entities.Contracts.DataStores;

public interface IVocationStore : IDataStore<byte, IVocation>, IDataStore
{
}