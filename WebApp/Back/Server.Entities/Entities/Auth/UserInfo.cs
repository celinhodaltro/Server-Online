namespace Server.Entities
{
    public class UserInfo : DefaultDb
    {
        public int PremiumTime { get; set; }
        public string Secret { get; set; }
        public bool AllowManyOnline { get; set; }
        public DateTime? BanishedAt { get; set; }
        public string BanishmentReason { get; set; }
        public uint? BannedBy { get; set; }
        public int UserId { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<UserVipList> VipList { get; set; }
    }
}