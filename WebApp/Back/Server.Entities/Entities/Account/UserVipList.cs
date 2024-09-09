namespace Server.Entities;
public sealed class UserVipList
{
    public int AccountId { get; set; }
    public int PlayerId { get; set; }
    public string Description { get; set; }
    public Player Player { get; set; }
}