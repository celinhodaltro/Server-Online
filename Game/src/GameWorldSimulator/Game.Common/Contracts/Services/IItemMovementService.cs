using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Results;

namespace Game.Common.Contracts.Services;

public interface IItemMovementService
{
    Result<OperationResultList<IItem>> Move(IPlayer player, IItem item, IHasItem from, IHasItem destination,
        byte amount,
        byte fromPosition, byte? toPosition);
}