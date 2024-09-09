using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.DataStores;

public interface IQuestDataStore : IDataStore<(ushort ActionId, uint UniqueId), QuestData>
{
}