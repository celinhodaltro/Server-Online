using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.DataStores;

public interface IGuildStore : IDataStore<ushort, IGuild>
{
}