using Extensions.Services;
using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Creatures;
using Game.Common.Location.Structs;
using Server.Common.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Commands;

public class TeleportToTempleCommand : CommandSpell
{
    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotEnoughRoom;

        var playerName = Params?.Length > 0 ? Params[0].ToString() : actor.Name;
        var gameManager = IoC.GetInstance<IGameCreatureManager>();

        if (!gameManager.TryGetPlayer(playerName, out var player))
        {
            error = InvalidOperation.PlayerNotFound;
            return false;
        }

        var location = new Location(player.Town.Coordinate);
        player.TeleportTo(location);
        EffectService.Send(location, EffectT.BubbleBlue);
        return true;
    }
}