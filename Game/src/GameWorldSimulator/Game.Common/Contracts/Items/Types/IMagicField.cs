using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Items.Types;

public interface IMagicField
{
    Location.Structs.Location Location { get; }
    IItemType Metadata { get; }

    void CauseDamage(ICreature toCreature);
}