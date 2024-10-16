using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;

namespace Server.Commands.Player;

public class PlayerLogOutCommand : ICommand
{
    private readonly IGameServer game;

    public PlayerLogOutCommand(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, bool forced = false)
    {
        if (!player.Logout(forced) && !forced) return;

        game.CreatureManager.RemovePlayer(player);
    }
}