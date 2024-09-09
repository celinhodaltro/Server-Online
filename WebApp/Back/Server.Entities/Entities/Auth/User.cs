using System;
using System.Collections.Generic;
using System.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class User : DefaultDb
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int PremiumTime { get; set; }
        public string Secret { get; set; }
        public bool AllowManyOnline { get; set; }
        public DateTime? BanishedAt { get; set; }
        public string BanishmentReason { get; set; }
        public uint? BannedBy { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<AccountVipList> VipList { get; set; }
    }
}