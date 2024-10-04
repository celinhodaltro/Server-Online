using System.Linq;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Game.Items.Items.Attributes;

namespace Game.Items.Factories.AttributeFactory;

public class ProtectionFactory
{
    public static IProtection Create(IItem item)
    {
        if (item.Metadata.Attributes.DamageProtection is not { } damageProtection) return null;
        if (!damageProtection.Any()) return null;

        return new Protection(item);
    }
}