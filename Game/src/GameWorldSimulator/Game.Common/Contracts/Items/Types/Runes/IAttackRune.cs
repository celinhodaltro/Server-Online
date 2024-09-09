using Game.Common.Contracts.Items.Types.Usable;
using Game.Common.Creatures;
using Game.Common.Item;

namespace Game.Common.Contracts.Items.Types.Runes;

public interface IAttackRune : IUsableAttackOnCreature, IUsableAttackOnTile, IRune
{
    /// <summary>
    ///     Rune's Damage Type
    /// </summary>
    DamageType DamageType { get; }

    /// <summary>
    ///     Damage Effect
    /// </summary>
    new EffectT Effect { get; }
}