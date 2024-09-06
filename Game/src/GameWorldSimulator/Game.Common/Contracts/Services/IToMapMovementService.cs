using Game.Common.Contracts.Creatures;
using Game.Common.Creatures.Structs;

namespace Game.Common.Contracts.Services;

public interface IToMapMovementService
{
    void Move(IPlayer player, MovementParams itemThrow);
}