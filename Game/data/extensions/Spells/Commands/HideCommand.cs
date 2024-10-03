using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;

namespace Extensions.Spells.Commands;

public class HideCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotEnoughRoom;
        actor.TurnInvisible();
        return true;
    }
}