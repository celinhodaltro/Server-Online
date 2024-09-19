
using Game.Common.Creatures.Players;

namespace Server.Entities;

public class CharacterOutfitAddon : DefaultDb
{
    public Character Player { get; set; }
    public int PlayerId { get; set; }
    public int LookType { get; set; }
    public OutfitAddon AddonLevel { get; set; } = OutfitAddon.None;
}