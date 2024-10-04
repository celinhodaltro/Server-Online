using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;

namespace Game.Items.Factories.AttributeFactory;

public class SkillBonusFactory : IFactory
{
    public event CreateItem OnItemCreated;

    public ISkillBonus Create(IItemType itemType)
    {
        //if (itemType.Attributes.SkillBonuses is not { } skillBonuses) return null;
        //if (!skillBonuses.Any()) return null;

        //return new SkillBonus();
        return null;
    }
}