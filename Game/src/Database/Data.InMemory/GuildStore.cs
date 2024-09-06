using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;

namespace Data.InMemory;

public class GuildStore : DataStore<GuildStore, ushort, IGuild>, IGuildStore
{
}