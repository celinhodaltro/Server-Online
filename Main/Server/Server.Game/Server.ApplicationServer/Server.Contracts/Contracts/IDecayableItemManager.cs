using Server.Entities.Common.Contracts.Items;

namespace Server.Contracts.Contracts;

public interface IDecayableItemManager
{
    void Add(IItem item);
    void DecayExpiredItems();
}