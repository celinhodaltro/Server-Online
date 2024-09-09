using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IVocationStore : IDataStore<byte, IVocation>, IDataStore
{
}