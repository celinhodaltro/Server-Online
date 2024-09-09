using Server.Entities.Contracts.Creatures;
using Server.Entities.Contracts.Items;

namespace Server.Entities.Contracts.Services;

public interface IDealTransaction
{
    bool Buy(IPlayer buyer, IShopperNpc seller, IItemType itemType, byte amount);
}