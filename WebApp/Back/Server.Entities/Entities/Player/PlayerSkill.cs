using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public sealed class PlayerSkill : DefaultDb
    {
        public int PlayerId { get; set; }
        public int SkillFist { get; set; }
        public double SkillFistTries { get; set; }

        public int SkillClub { get; set; }
        public double SkillClubTries { get; set; }

        public int SkillSword { get; set; }
        public double SkillSwordTries { get; set; }

        public int SkillAxe { get; set; }
        public double SkillAxeTries { get; set; }

        public int SkillDist { get; set; }
        public double SkillDistTries { get; set; }

        public int SkillShielding { get; set; }
        public double SkillShieldingTries { get; set; }

        public int SkillFishing { get; set; }
        public double SkillFishingTries { get; set; }
        public int MagicLevel { get; set; }
        public double MagicLevelTries { get; set; }

    }
}
