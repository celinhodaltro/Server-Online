using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts;

public interface IGuildStore : IDataStore<ushort, IGuild>
{
}