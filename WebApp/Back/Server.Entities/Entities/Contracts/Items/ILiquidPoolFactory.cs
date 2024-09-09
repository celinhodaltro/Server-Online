using Game.Common.Contracts.Items.Types;
using Game.Common.Item;

namespace Game.Common.Contracts.Items;

public interface ILiquidPoolFactory : IFactory
{
    ILiquid Create(Location.Structs.Location location, LiquidColor color);
    ILiquid CreateDamageLiquidPool(Location.Structs.Location location, LiquidColor color);
}