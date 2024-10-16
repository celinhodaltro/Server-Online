﻿using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.Items.Types.Containers;

namespace Game.Items.Items.Containers.Container.Queries;

internal static class FindSlotOfFirstItemNotFullyQuery
{
    public static int Find(IContainer onContainer, ICumulative cumulativeItem)
    {
        var itemToJoinSlot = -1;

        for (var slotIndex = 0; slotIndex < onContainer.SlotsUsed; slotIndex++)
        {
            var itemOnSlot = onContainer.Items[slotIndex];
            if (itemOnSlot.ClientId == cumulativeItem?.ClientId && (itemOnSlot as ICumulative)?.Amount < 100)
            {
                itemToJoinSlot = slotIndex;
                break;
            }
        }

        return itemToJoinSlot;
    }
}