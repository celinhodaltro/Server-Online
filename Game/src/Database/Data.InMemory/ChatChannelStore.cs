using Game.Common.Contracts.Chats;
using Game.Common.Contracts.DataStores;

namespace Data.InMemory;

public class ChatChannelStore : DataStore<ChatChannelStore, ushort, IChatChannel>, IChatChannelStore
{
}