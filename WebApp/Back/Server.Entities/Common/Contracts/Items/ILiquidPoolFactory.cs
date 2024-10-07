using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items;

public interface ILiquidPoolFactory : IFactory
{
    ILiquid Create(Location.Structs.Location location, LiquidColor color);
    ILiquid CreateDamageLiquidPool(Location.Structs.Location location, LiquidColor color);
}