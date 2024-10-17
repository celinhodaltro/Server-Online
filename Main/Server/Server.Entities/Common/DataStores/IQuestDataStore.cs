using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts;

public interface IQuestDataStore : IDataStore<(ushort ActionId, uint UniqueId), QuestData>
{
}