using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.Creatures;

public interface IShopItem
{
    IItemType Item { get; }
    uint BuyPrice { get; }
    uint SellPrice { get; }
}