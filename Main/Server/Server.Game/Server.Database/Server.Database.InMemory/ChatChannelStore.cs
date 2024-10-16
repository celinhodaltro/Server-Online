using Server.Entities.Common.Contracts.Chats;
using Server.Entities.Common.Contracts;

namespace Data.InMemory;

public class ChatChannelStore : DataStore<ChatChannelStore, ushort, IChatChannel>, IChatChannelStore
{
}