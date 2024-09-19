namespace Server.Entities;

public class CharacterQuest : DefaultDb
{
    public int PlayerId { get; set; }
    public Character Player { get; set; }
    public string Group { get; set; }
    public string GroupKey { get; set; }
    public int ActionId { get; set; }
    public int UniqueId { get; set; }
    public string Name { get; set; }
    public bool Done { get; set; }
}