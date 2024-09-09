namespace Server.Entities;

public sealed class GuildRank : DefaultDb
{
    public int GuildId { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public Guild Guild { get; set; }
}