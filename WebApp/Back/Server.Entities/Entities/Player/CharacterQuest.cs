namespace Server.Entities;

public class CharacterQuest : DefaultDb
{
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    public string Group { get; set; }
    public string GroupKey { get; set; }
    public int ActionId { get; set; }
    public int UniqueId { get; set; }
    public string Name { get; set; }
    public bool Done { get; set; }
}