using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.Services;

public interface IDealTransaction
{
    bool Buy(IPlayer buyer, IShopperNpc seller, IItemType itemType, byte amount);
}