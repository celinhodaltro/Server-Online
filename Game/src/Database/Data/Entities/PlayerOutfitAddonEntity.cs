using Game.Common.Creatures.Players;

namespace Data.Entities;

public class PlayerOutfitAddonEntity
{
    public PlayerEntity Player { get; set; }
    public int PlayerId { get; set; }
    public int LookType { get; set; }
    public OutfitAddon AddonLevel { get; set; } = OutfitAddon.None;
}