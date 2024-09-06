using Game.Common.Contracts.DataStores;
using Game.Common.Item;

namespace Data.InMemory;

public class QuestDataDataStore : DataStore<QuestDataDataStore, (ushort ActionId, uint UniqueId), QuestData>,
    IQuestDataStore
{
}