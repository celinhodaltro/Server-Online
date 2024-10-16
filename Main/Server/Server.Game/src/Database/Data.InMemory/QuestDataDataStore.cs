using Server.Entities.Common.Contracts;
using Server.Entities.Common.Item;

namespace Data.InMemory;

public class QuestDataDataStore : DataStore<QuestDataDataStore, (ushort ActionId, uint UniqueId), QuestData>,
    IQuestDataStore
{
}