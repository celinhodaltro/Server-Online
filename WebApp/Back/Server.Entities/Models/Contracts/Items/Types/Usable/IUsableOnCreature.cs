using Server.Entities.Models.Combat.Structs;
using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Items.Types.Usable;

public interface IUsableOnCreature : IUsableOn
{
    /// <summary>
    ///     Useable by players on creatures
    /// </summary>
    /// <param name="usedBy">creature whose item is being used</param>
    /// <param name="creature"></param>
    public void Use(IPlayer usedBy, ICreature creature);
}

public interface IUsableAttackOnCreature : IUsableOn
{
    bool NeedTarget { get; }

    /// <summary>
    ///     Useable by creatures to attack creatures
    /// </summary>
    /// <param name="usedBy">creature whose item is being used</param>
    /// <param name="creature"></param>
    /// <param name="combat"></param>
    public bool Use(ICreature usedBy, ICreature creature, out CombatAttackResult combat);
}