using Server.Entities.Common;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Containers;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Item;
using Server.Entities.Common.Results;

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