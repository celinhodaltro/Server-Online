using Game.Common.Item;

namespace Game.Common.Contracts.DataStores;

public interface IQuestDataStore : IDataStore<(ushort ActionId, uint UniqueId), QuestData>
{
}