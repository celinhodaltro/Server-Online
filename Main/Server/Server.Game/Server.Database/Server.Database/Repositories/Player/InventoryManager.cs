using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Entities;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Helpers;

namespace Data.Repositories.Player;

internal static class InventoryManager
{
    public static async Task SaveBackpack(IPlayer player, NeoContext neoContext)
    {
        if (Guard.AnyNull(player, player.Inventory?.BackpackSlot)) return;

        if (player.Inventory?.BackpackSlot?.Items?.Count == 0) return;

        neoContext.PlayerItems.RemoveRange(neoContext.PlayerItems.Where(x => x.PlayerId == player.Id));

        await ContainerManager.Save<Server.Entities.CharacterItem>(player, player.Inventory?.BackpackSlot, neoContext);
    }

    public static async Task SavePlayerInventory(IPlayer player, NeoContext neoContext)
    {
        var playerInventory = await neoContext
            .PlayerInventoryItems
            .Where(x => x.PlayerId == player.Id)
            .ToDictionaryAsync(x => x.SlotId);

        foreach (var slot in new[]
                 {
                     Slot.Necklace, Slot.Head, Slot.Backpack, Slot.Left, Slot.Body, Slot.Right, Slot.Ring, Slot.Legs,
                     Slot.Ammo, Slot.Feet
                 })
        {
            var item = player.Inventory[slot];

            if (playerInventory.TryGetValue((int)slot, out var playerInventoryItemEntity))
            {
                playerInventoryItemEntity.ServerId = item?.Metadata?.TypeId ?? 0;
                playerInventoryItemEntity.Amount = item?.Amount ?? 0;
                playerInventoryItemEntity.PlayerId = (int)player.Id;
                playerInventoryItemEntity.SlotId = (int)slot;

                neoContext.PlayerInventoryItems.Update(playerInventoryItemEntity);
                continue;
            }

            await neoContext.PlayerInventoryItems.AddAsync(new PlayerInventoryItemEntity
            {
                Amount = 0, PlayerId = (int)player.Id, SlotId = (int)slot, ServerId = 0
            });
        }
    }
}