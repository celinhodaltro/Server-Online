using Server.Entities.Common.Contracts.Chats;

namespace Server.Entities.Common.Contracts;

public interface IChatChannelStore : IDataStore<ushort, IChatChannel>
{
}