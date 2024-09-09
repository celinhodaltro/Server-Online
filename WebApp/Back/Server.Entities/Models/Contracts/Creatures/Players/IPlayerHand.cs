using Server.Entities.Models.Contracts;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.World.Tiles;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.Creatures.Players;

public interface IPlayerHand
{
    Result<OperationResultList<IItem>> Move(IItem item, IHasItem from, IHasItem destination, byte amount,
        byte fromPosition, byte? toPosition);

    Result<OperationResultList<IItem>> PickItemFromGround(IItem item, ITile tile, byte amount = 1);
}