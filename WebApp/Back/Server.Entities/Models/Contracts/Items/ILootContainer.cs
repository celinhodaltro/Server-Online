using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types.Containers;

namespace Server.Entities.Models.Contracts.Items;

public interface ILootContainer : IContainer
{
    ILoot Loot { get; }
    bool LootCreated { get; }

    bool CanBeOpenedBy(IPlayer player);
    void MarkAsLootCreated();
}