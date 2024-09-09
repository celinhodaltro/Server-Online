using Server.Entities.Models.Location.Structs;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items;

public interface ILiquidPoolFactory : IFactory
{
    ILiquid Create(Location.Structs.Location location, LiquidColor color);
    ILiquid CreateDamageLiquidPool(Location.Structs.Location location, LiquidColor color);
}