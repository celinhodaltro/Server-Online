using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface IFood : IItem
{
    public ushort Duration => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.Duration);
}