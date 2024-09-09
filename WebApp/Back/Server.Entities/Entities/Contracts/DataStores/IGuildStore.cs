using Server.Entities.Contracts.Creatures;

namespace Server.Entities.Contracts.DataStores;

public interface IGuildStore : IDataStore<ushort, IGuild>
{
}