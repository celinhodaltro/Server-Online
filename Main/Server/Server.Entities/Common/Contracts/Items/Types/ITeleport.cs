using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface ITeleport
{
    bool HasDestination { get; }
    bool Teleport(IWalkableCreature player);
}