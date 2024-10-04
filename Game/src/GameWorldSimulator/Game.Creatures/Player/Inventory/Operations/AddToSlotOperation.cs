using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Results;
using Game.Creatures.Player.Inventory.Rules;

namespace Game.Creatures.Player.Inventory.Operations;

public abstract class AddToSlotOperation
{
    public static Result<IItem> Add(Inventory inventory, Slot slot, IItem item)
    {
        var result = inventory.CanAddItem(slot, item, item.Amount);

        if (result.Failed) return Result<IItem>.Fail(result.Error);

        if (SwapRule.ShouldSwap(inventory, item, slot)) return SwapOperation.SwapItem(inventory, slot, item);

        if (slot is Slot.Backpack) return AddToBackpackOperation.Add(inventory, item);

        if (item is ICumulative cumulative)
        {
            var addCumulativeResult = AddCumulativeItemOperation.Add(inventory, cumulative, slot);

            if (result.Failed) return Result<IItem>.Fail(addCumulativeResult.Error);
        }

        inventory.InventoryMap.Add(slot, item, item.ClientId);

        item.SetNewLocation(Location.Inventory(slot));

        if (item is IDressable dressable) dressable.DressedIn(inventory.Owner);

        item.SetParent(inventory.Owner);

        return new Result<IItem>();
    }
}