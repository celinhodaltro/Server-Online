using Server.Entities.Contracts.Chats;

namespace Server.Entities.Contracts.DataStores;

public interface IChatChannelStore : IDataStore<ushort, IChatChannel>
{
}