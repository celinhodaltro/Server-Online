using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class MassKickPlayersCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        var ctx = IoC.GetInstance<IGameCreatureManager>();
        var playerLogOutCommand = IoC.GetInstance<PlayerLogOutCommand>();

        foreach (var player in ctx.GetAllLoggedPlayers())
        {
            if (player is null || player.CreatureId == actor.CreatureId)
                continue;

            playerLogOutCommand.Execute(player, true);
        }

        error = InvalidOperation.None;
        return true;
    }
}