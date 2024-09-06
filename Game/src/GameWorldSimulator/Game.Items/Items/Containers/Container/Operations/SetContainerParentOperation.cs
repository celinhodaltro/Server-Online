using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Creatures.Players;
using Game.Common.Location.Structs;

namespace Game.Items.Items.Containers.Container.Operations;

internal static class SetContainerParentOperation
{
    public static void SetParent(Container container, IThing thing)
    {
        container.Parent = thing;
        if (container.Parent is IPlayer) container.Location = new Location(Slot.Backpack);
        container.SetOwner(container.RootParent);
    }
}