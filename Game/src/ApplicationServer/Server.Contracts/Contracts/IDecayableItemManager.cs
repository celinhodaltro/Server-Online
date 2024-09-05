using Game.Common.Contracts.Items;

namespace Server.Common.Contracts;

public interface IDecayableItemManager
{
    void Add(IItem item);
    void DecayExpiredItems();
}