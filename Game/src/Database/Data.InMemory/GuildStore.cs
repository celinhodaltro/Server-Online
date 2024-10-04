using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;

namespace Data.InMemory;

public class GuildStore : DataStore<GuildStore, ushort, IGuild>, IGuildStore
{
}