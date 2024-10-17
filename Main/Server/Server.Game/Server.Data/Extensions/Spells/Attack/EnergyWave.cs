using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;

namespace Extensions.Spells.Attack;

public class EnergyWave : WaveSpell
{
    public override DamageType DamageType => DamageType.Energy;
    public override EffectT DamageEffect => EffectT.BubbleBlue;
    protected override string AreaName => "AREA_SQUAREWAVE5";

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }
}