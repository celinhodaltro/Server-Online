using Server.Entities.Item;

namespace Server.Entities.Contracts.DataStores;

public interface IQuestDataStore : IDataStore<(ushort ActionId, uint UniqueId), QuestData>
{
}