using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types;

public interface ILiquid : IItem
{
    bool IsLiquidPool { get; }

    bool IsLiquidSource { get; }

    bool IsLiquidContainer { get; }
    LiquidColor LiquidColor { get; }
}