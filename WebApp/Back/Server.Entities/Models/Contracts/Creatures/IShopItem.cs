using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Creatures;

public interface IShopItem
{
    IItemType Item { get; }
    uint BuyPrice { get; }
    uint SellPrice { get; }
}