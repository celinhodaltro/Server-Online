namespace Server.Entities;

public class PlayerOutfitAddon
{
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public int LookType { get; set; }
    public OutfitAddon AddonLevel { get; set; } = OutfitAddon.None;
}