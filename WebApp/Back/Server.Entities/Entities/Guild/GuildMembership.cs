namespace Data.Entities;

public sealed class GuildMembership
{
    public int PlayerId { get; set; }
    public int GuildId { get; set; }
    public int RankId { get; set; }
    public string Nick { get; set; }

    public PlayerEntity Player { get; set; }
    public Guild Guild { get; set; }
    public GuildRank Rank { get; set; }
}