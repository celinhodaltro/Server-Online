
using Server.Entities.Common.Creatures.Players;

namespace Server.Entities;

public class CharacterOutfitAddon : DefaultDb
{
    public Character Character { get; set; }
    public int CharacterId { get; set; }
    public int LookType { get; set; }
    public OutfitAddon AddonLevel { get; set; } = OutfitAddon.None;
}