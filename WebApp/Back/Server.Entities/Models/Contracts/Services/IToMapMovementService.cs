using Server.Entities.Models.Creatures.Structs;
using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Services;

public interface IToMapMovementService
{
    void Move(IPlayer player, MovementParams itemThrow);
}