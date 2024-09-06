using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class KickPlayerCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        var commands = words.Split("/kick");

        if (string.IsNullOrWhiteSpace(commands[1]))
        {
            error = InvalidOperation.NotPossible;
            return false;
        }

        var ctx = IoC.GetInstance<IGameCreatureManager>();

        if (!ctx.TryGetPlayer(commands[1], out var player))
        {
            error = InvalidOperation.NotPossible;
            return false;
        }

        if (player is null || player.CreatureId == actor.CreatureId)
        {
            error = InvalidOperation.NotPossible;
            return false;
        }

        var playerLogOutCommand = IoC.GetInstance<PlayerLogOutCommand>();
        playerLogOutCommand.Execute(player, true);
        error = InvalidOperation.None;
        return true;
    }
}