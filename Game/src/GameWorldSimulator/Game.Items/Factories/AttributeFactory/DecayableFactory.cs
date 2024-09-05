using Game.Common.Contracts.Items;
using Game.Common.Helpers;
using Game.Items.Items.Attributes;

namespace Game.Items.Factories.AttributeFactory;

public class DecayableFactory
{
    public static IDecayable CreateIfItemIsDecayable(IItem item)
    {
        if (Guard.AnyNull(item)) return null;

        return item.HasDecayBehavior ? new Decayable(item) : null;
    }
}