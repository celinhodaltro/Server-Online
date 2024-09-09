namespace Server.Entities;

public abstract class PlayerItemBase
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
}