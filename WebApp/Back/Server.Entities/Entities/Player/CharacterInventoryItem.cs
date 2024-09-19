namespace Server.Entities;

public sealed class CharacterInventoryItem : DefaultDb
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int ServerId { get; set; }
    public int SlotId { get; set; }
    public short Amount { get; set; }

    public Character Player { get; set; }
}