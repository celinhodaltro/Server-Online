using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Location;
using Server.Entities.Common.Results;
using Server.Entities.Common.Services;
using Server.Entities.Common.Texts;

namespace Game.Creatures.Services;

public class ItemMovementService : IItemMovementService
{
    private readonly IWalkToMechanism _walkToMechanism;

    public ItemMovementService(IWalkToMechanism walkToMechanism)
    {
        _walkToMechanism = walkToMechanism;
    }

    public Result<OperationResultList<IItem>> Move(IPlayer player, IItem item, IHasItem from, IHasItem destination,
        byte amount,
        byte fromPosition, byte? toPosition)
    {
        if (player is null) return Result<OperationResultList<IItem>>.NotPossible;

        if (item.Location.Type == LocationType.Ground)
        {
            if (item.Location.Z < player.Location.Z)
            {
                OperationFailService.Send(player.CreatureId, TextConstants.FIRST_GO_UPSTAIRS);
                return Result<OperationResultList<IItem>>.NotPossible;
            }

            if (item.Location.Z > player.Location.Z)
            {
                OperationFailService.Send(player.CreatureId, TextConstants.FIRST_GO_DOWNSTAIRS);
                return Result<OperationResultList<IItem>>.NotPossible;
            }
        }

        if (!item.IsCloseTo(player))
        {
            _walkToMechanism.WalkTo(player,
                () => player.MoveItem(item, from, destination, amount, fromPosition, toPosition), item.Location);
            return Result<OperationResultList<IItem>>.Success;
        }

        return player.MoveItem(item, from, destination, amount, fromPosition, toPosition);
    }
}