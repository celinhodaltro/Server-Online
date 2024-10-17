using System.Collections.Generic;
using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Creatures;

public delegate void ShowShop(INpc npc, ISociableCreature to, IEnumerable<IShopItem> shopItems);

public interface IShopperNpc : INpc
{
    IDictionary<ushort, IShopItem> ShopItems { get; }

    event ShowShop OnShowShop;

    void StopSellingToCustomer(ISociableCreature creature);
    bool BuyFromCustomer(ISociableCreature creature, IItemType item, byte amount);
    ulong CalculateCost(IItemType itemType, byte amount);
}