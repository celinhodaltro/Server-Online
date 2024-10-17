using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types.Usable;

public delegate void Use(ICreature usedBy, ICreature creature, IItem item);

public interface IConsumable : IConsumableRequirement, IUsableOnCreature
{
    public string Sentence => Metadata.Attributes.GetAttribute(ItemAttribute.Sentence);
    public event Use OnUsed;
}