using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;

namespace Server.Entities.Common.Contracts.Services;

public interface IDealTransaction
{
    bool Buy(IPlayer buyer, IShopperNpc seller, IItemType itemType, byte amount);
}