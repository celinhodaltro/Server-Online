using Server.Entities.Models.Contracts.Chats;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IChatChannelStore : IDataStore<ushort, IChatChannel>
{
}