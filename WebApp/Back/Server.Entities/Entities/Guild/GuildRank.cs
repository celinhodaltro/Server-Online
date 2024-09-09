namespace Data.Entities;

public sealed class GuildRank
{
    public int Id { get; set; }
    public int GuildId { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public Guild Guild { get; set; }
}