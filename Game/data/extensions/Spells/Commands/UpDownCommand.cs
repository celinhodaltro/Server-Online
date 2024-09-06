using System;
using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.World.Map;

namespace Extensions.Spells.Commands;

public class UpDownCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotEnoughRoom;

        var location = actor.Location;

        if (words.Equals("/up", StringComparison.InvariantCultureIgnoreCase))
        {
            if (actor.Location.Z == byte.MinValue) return false;
            location.Update(location.X, location.Y, (byte)(location.Z - 1));
        }
        else
        {
            if (actor.Location.Z >= 15) return false;
            location.Update(location.X, location.Y, (byte)(location.Z + 1));
        }

        return Map.Instance.TryMoveCreature(actor, location);
    }
}