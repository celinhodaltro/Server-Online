using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Loader.Guilds;
using Loader.Interfaces;
using Server.Common.Contracts;
using Server.Common.Contracts.Commands;
using Server.Common.Contracts.Network;
using Server.Entities;
using Serilog;
using Loader.Players;

namespace Server.Commands.Player;

public class PlayerLogInCommand : ICommand
{
    private readonly ILogger _logger;
    private readonly IGameServer game;
    private readonly GuildLoader guildLoader;
    private readonly IEnumerable<PlayerLoader> playerLoaders;

    public PlayerLogInCommand(IGameServer game, IEnumerable<PlayerLoader> playerLoader, GuildLoader guildLoader,
        ILogger logger)
    {
        this.game = game;
        this.playerLoaders = playerLoader;
        this.guildLoader = guildLoader;
        _logger = logger;
    }

    public void Execute(Server.Entities.Character playerRecord, IConnection connection)
    {
        if (playerRecord is null)
            //todo validations here
            return;

        if (!game.CreatureManager.TryGetLoggedPlayer((uint)playerRecord.Id, out var player))
        {
            if (playerLoaders.FirstOrDefault(x => x.IsApplicable(playerRecord)) is not { } playerLoader)
                return;

            guildLoader.Load(playerRecord.GuildMember?.Guild);
            player = playerLoader.Load(playerRecord);
        }

        game.CreatureManager.AddPlayer(player, connection);

        player.Login();
        player.Vip.LoadVipList(playerRecord.Account.UserInfo.VipList.Select(x => ((uint)x.PlayerId, x.Player?.Name)));
        _logger.Information("Player {PlayerName} logged in", player.Name);
    }
}