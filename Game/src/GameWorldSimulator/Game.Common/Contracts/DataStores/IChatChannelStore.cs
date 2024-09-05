using Game.Common.Contracts.Chats;

namespace Game.Common.Contracts.DataStores;

public interface IChatChannelStore : IDataStore<ushort, IChatChannel>
{
}