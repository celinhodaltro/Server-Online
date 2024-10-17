using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface ILiquid : IItem
{
    bool IsLiquidPool { get; }

    bool IsLiquidSource { get; }

    bool IsLiquidContainer { get; }
    LiquidColor LiquidColor { get; }
}