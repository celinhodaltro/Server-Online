namespace Server.Entities;

public sealed class GuildMembership : DefaultDb
{
    public int PlayerId { get; set; }
    public int GuildId { get; set; }
    public int RankId { get; set; }
    public string Nick { get; set; }

    public Player Player { get; set; }
    public Guild Guild { get; set; }
    public GuildRank Rank { get; set; }
}