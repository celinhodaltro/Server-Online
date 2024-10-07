using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Creatures;

public interface IShopItem
{
    IItemType Item { get; }
    uint BuyPrice { get; }
    uint SellPrice { get; }
}