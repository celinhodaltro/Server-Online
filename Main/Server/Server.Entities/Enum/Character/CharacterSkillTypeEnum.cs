using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities.Enum
{
    public enum CharacterMasteryType
    {
        #region Magic
        Pyromancing, Cryomancing, Aeromancing, Electromancing, Necromancy,
        Druidism, Clericism, Musician,
        #endregion

        #region Weapon_Malee
        ShortSword, LongSword, DualSword,
        ShortAxe, LongAxe, DualAxe,
        ShortHammer, LongHammer,
        Dagger, DualDagger,
        Lance,
        #endregion

        #region Weapon_Range
        LongBow, Crossbow, ThrowingKnife, Sling
        #endregion 
    }
}
