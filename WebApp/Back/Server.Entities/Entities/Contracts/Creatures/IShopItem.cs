using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.Creatures;

public interface IShopItem
{
    IItemType Item { get; }
    uint BuyPrice { get; }
    uint SellPrice { get; }
}