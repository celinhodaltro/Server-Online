using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities.Enum
{
    enum CharacterMasteryType
    {
        #region Magic
        Pyromance = 0, 
        Cryomance = 1, 
        Musician = 2, 
        Wizard = 3, 
        Druid = 4, 
        Necromancer = 5, 
        Sorcerer = 6, 
        Illusionist = 8,
        Cleric = 9, 
        Enchanter = 10, 
        Summoner = 11, 
        #endregion

        #region Weapon_Malee
        ShortSword = 5,
        LongSword  = 6,
        DualSword  = 7,

        ShortAxe = 8,
        LongAxe  = 9,
        DualAxe  = 10,

        ShortHammer = 11,
        LongHammer  = 12,

        Lance = 13,

        Dagger = 14,
        DualDagger = 15,
        #endregion

        #region Weapon_Range
        LongBow = 15,
        Crossbow = 16,
        ThrowingKnife = 17,
        Sling = 18
        #endregion 
    }
}
