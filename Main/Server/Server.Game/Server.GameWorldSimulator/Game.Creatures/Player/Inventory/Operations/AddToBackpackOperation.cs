using Server.Entities.Common;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Results;

namespace Game.Creatures.Player.Inventory.Operations;

public static class AddToBackpackOperation
{
    public static Result<IItem> Add(Inventory inventory, IItem item)
    {
        if (!item.IsPickupable) return Result<IItem>.Fail(InvalidOperation.CannotDress);

        if (inventory.InventoryMap.GetItem<IContainer>(Slot.Backpack) is { } backpack)
            return new Result<IItem>(null, backpack.AddItem(item).Error);

        AddBackpackParent(inventory, item);

        inventory.InventoryMap.Add(Slot.Backpack, item, item.ClientId);

        item.SetNewLocation(Location.Inventory(Slot.Backpack));

        return Result<IItem>.Success;
    }

    private static void AddBackpackParent(Inventory inventory, IItem item)
    {
        if (item is not IContainer container) return;
        if (item is IContainer { IsPickupable: false }) return;

        container.SetParent(inventory.Owner);
        container.SubscribeToWeightChangeEvent(inventory.ContainerOnOnWeightChanged);
    }
}