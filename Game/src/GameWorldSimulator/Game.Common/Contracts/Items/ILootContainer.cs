using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types.Containers;

namespace Game.Common.Contracts.Items;

public interface ILootContainer : IContainer
{
    ILoot Loot { get; }
    bool LootCreated { get; }

    bool CanBeOpenedBy(IPlayer player);
    void MarkAsLootCreated();
}