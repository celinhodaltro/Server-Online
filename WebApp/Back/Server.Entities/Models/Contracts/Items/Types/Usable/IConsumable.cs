using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Usable;

public delegate void Use(ICreature usedBy, ICreature creature, IItem item);

public interface IConsumable : IConsumableRequirement, IUsableOnCreature
{
    public string Sentence => Metadata.Attributes.GetAttribute(ItemAttribute.Sentence);
    public event Use OnUsed;
}