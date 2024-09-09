using Game.Common.Creatures;
using Server.Entities.Models.Contracts.Items.Types.Usable;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Runes;

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