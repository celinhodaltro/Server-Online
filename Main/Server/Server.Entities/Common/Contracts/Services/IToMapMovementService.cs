using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Structs;

namespace Server.Entities.Common.Contracts.Services;

public interface IToMapMovementService
{
    void Move(IPlayer player, MovementParams itemThrow);
}