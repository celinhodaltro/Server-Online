using Game.Common.Item;

namespace Server.Entities;

public abstract class PlayerItemBase : DefaultDb
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public short Amount { get; set; }
    public int ParentId { get; set; }
    public int ServerId { get; set; }

    public virtual Player Player { get; set; }
    public ushort? DecayTo { get; set; }
    public uint? DecayDuration { get; set; }
    public uint? DecayElapsed { get; set; }

    public ushort? Charges { get; set; }
    public int ContainerId { get; set; } = 0;

    public Dictionary<ItemAttribute, IConvertible> GetAttributes()
    {
        var attributes = new Dictionary<ItemAttribute, IConvertible>
        {
            { ItemAttribute.Count, this.Amount }
        };

        if (this.Charges > 0) attributes.Add(ItemAttribute.Charges, this.Charges);

        if (this.DecayDuration > 0)
        {
            attributes.Add(ItemAttribute.DecayTo, this.DecayTo);
            attributes.Add(ItemAttribute.DecayElapsed, this.DecayElapsed);
            attributes.Add(ItemAttribute.Duration, this.DecayDuration);
        }

        return attributes;
    }
}