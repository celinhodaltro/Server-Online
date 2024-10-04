using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;

namespace Game.Creatures.Npcs.Shop;

public record ShopItem(IItemType Item, uint BuyPrice, uint SellPrice) : IShopItem
{
}