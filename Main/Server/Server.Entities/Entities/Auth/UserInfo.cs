using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace Server.Entities
{
    public class UserInfo : DefaultDb
    {
        public int PremiumTime { get; set; } = 0;
        public string? Secret { get; set; } = Guid.NewGuid().ToString();
        public bool AllowManyOnline { get; set; } = false;
        public DateTime? BanishedAt { get; set; } 
        public string? BanishmentReason { get; set; } = string.Empty;
        public uint? BannedBy { get; set; } = 0;

        public int? UserId { get; set; } = 0;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<UserVipList>? VipList { get; set; }
  

    }
}