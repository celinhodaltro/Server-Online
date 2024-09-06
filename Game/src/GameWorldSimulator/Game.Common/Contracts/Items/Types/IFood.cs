using Game.Common.Item;

namespace Game.Common.Contracts.Items.Types;

public interface IFood : IItem
{
    public ushort Duration => Metadata.Attributes.GetAttribute<ushort>(ItemAttribute.Duration);
}