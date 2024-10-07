using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface IMagicField
{
    Location.Structs.Location Location { get; }
    IItemType Metadata { get; }

    void CauseDamage(ICreature toCreature);
}