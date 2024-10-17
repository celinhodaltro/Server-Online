using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Item;

namespace Extensions.Spells.Attack;

public class FireWave : WaveSpell
{
    protected override string AreaName => "AREA_WAVE4";
    public override DamageType DamageType => DamageType.Fire;
    public override byte Range => 1;

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }
}