using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Items.Types;

public interface ITeleport
{
    bool HasDestination { get; }
    bool Teleport(IWalkableCreature player);
}