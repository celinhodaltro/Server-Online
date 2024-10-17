using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Contracts.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class PullPlayerCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotEnoughRoom;
        if (Params?.Length == 0) return false;

        var gameManager = IoC.GetInstance<IGameCreatureManager>();

        if (!gameManager.TryGetPlayer(Params[0].ToString(), out var player))
        {
            error = InvalidOperation.PlayerNotFound;
            return false;
        }

        var newLocation = actor.Location.GetNextLocation(actor.Direction);

        player.TeleportTo(newLocation);
        return true;
    }
}