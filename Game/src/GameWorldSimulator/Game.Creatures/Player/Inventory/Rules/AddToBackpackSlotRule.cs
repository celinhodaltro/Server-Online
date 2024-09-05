using Game.Common;
using Game.Common.Contracts.Items;
using Game.Common.Contracts.Items.Types.Containers;
using Game.Common.Creatures.Players;
using Game.Common.Item;
using Game.Common.Results;

namespace Game.Creatures.Player.Inventory.Rules;

public static class AddToBackpackSlotRule
{
    internal static Result CanAddToBackpackSlot(this Inventory inventory, IItem item)
    {
        if (item is IContainer container &&
            container.IsPickupable &&
            !inventory.InventoryMap.HasItemOnSlot(Slot.Backpack) &&
            item.Metadata.Attributes.GetAttribute(ItemAttribute.BodyPosition) == "backpack")
            return Result.Success;

        return inventory.InventoryMap.HasItemOnSlot(Slot.Backpack)
            ? Result.Success
            : Result.Fail(InvalidOperation.CannotDress);
    }
}