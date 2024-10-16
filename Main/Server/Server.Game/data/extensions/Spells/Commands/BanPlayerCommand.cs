﻿using Data.Interfaces;
using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class BanPlayerCommand : CommandSpell
{
    private const string BANISH_REASON = "You have been banished by a gamemaster.";

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotPossible;

        if (Params.Length == 0)
            return false;

        var ctx = IoC.GetInstance<IGameCreatureManager>();
        var playerLogOutCommand = IoC.GetInstance<PlayerLogOutCommand>();
        var accountRepository = IoC.GetInstance<IAccountRepository>();

        if (!ctx.TryGetPlayer(Params[0].ToString(), out var player))
            return false;

        if (player is null || player.CreatureId == actor.CreatureId)
            return false;

        var reason = Params[1]?.ToString() ?? BANISH_REASON;

        accountRepository.Ban(player.AccountId, reason, ((IPlayer)actor).AccountId).Wait();
        playerLogOutCommand.Execute(player, true);

        return true;
    }
}