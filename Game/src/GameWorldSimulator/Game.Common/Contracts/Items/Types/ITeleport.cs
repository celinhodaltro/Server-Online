using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Items.Types;

public interface ITeleport
{
    bool HasDestination { get; }
    bool Teleport(IWalkableCreature player);
}