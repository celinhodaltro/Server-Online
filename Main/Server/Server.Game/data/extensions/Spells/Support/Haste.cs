using Game.Combat.Spells;
using Server.Entities.Common.Creatures;

namespace Extensions.Spells.Support;

public class Haste : HasteSpell
{
    public override EffectT Effect => EffectT.GlitterBlue;
    public override uint Duration => 20000;
    public override ushort SpeedBoost => 400;
}