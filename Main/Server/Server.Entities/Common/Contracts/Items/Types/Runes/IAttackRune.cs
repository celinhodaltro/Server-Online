using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types.Runes;

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