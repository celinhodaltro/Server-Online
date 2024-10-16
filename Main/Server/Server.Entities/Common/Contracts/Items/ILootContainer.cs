using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types.Containers;

namespace Server.Entities.Common.Contracts.Items;

public interface ILootContainer : IContainer
{
    ILoot Loot { get; }
    bool LootCreated { get; }

    bool CanBeOpenedBy(IPlayer player);
    void MarkAsLootCreated();
}