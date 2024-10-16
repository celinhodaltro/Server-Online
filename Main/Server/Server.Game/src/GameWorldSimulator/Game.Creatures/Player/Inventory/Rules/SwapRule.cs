using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Creatures.Players;

namespace Game.Creatures.Player.Inventory.Rules;

public abstract class SwapRule
{
    public static bool ShouldSwap(Inventory inventory, IItem itemToAdd, Slot slotDestination)
    {
        if (slotDestination == Slot.Backpack) return false;

        if (inventory.InventoryMap.GetItem<IItem>(slotDestination) is not { } itemOnSlot) return false;

        if (itemToAdd is ICumulative cumulative && itemOnSlot.ClientId == cumulative.ClientId &&
            itemOnSlot.Amount + itemToAdd.Amount <= 100)
            //will join
            return false;

        return true;
    }
}