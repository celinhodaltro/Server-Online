using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;

namespace Game.Creatures.Npcs.Shop;

public record ShopItem(IItemType Item, uint BuyPrice, uint SellPrice) : IShopItem
{
}