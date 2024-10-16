using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Location.Structs;

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