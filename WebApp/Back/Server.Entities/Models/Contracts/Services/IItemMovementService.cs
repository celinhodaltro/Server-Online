using Server.Entities.Models.Contracts;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.Services;

public interface IItemMovementService
{
    Result<OperationResultList<IItem>> Move(IPlayer player, IItem item, IHasItem from, IHasItem destination,
        byte amount,
        byte fromPosition, byte? toPosition);
}