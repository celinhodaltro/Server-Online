using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.Services;

public interface IDealTransaction
{
    bool Buy(IPlayer buyer, IShopperNpc seller, IItemType itemType, byte amount);
}