using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.Creatures.Players;

public interface IPlayerHand
{
    Result<OperationResultList<IItem>> Move(IItem item, IHasItem from, IHasItem destination, byte amount,
        byte fromPosition, byte? toPosition);

    Result<OperationResultList<IItem>> PickItemFromGround(IItem item, ITile tile, byte amount = 1);
}