using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IGuildStore : IDataStore<ushort, IGuild>
{
}