using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types;

public interface IFood : IItem
{
    public ushort Duration => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.Duration);
}