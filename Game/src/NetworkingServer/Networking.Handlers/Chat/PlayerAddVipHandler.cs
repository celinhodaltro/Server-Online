using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Game.Common.Contracts.Creatures;
using Game.Common.Helpers;
using Loader.Interfaces;
using Networking.Packets.Incoming.Chat;
using Networking.Packets.Outgoing;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Tasks;
using Serilog;
using Loader.Players;
using Server.BusinessRules;

namespace Networking.Handlers.Chat;

public class PlayerAddVipHandler : PacketHandler
{
    private readonly IGameServer _game;
    private readonly ILogger _logger;
    private readonly IEnumerable<PlayerLoader> _playerLoader;
    private readonly CharacterBusinessRules PlayerBusinessRules;

    public PlayerAddVipHandler(IGameServer game, CharacterBusinessRules PlayerBusinessRules,
        IEnumerable<PlayerLoader> playerLoader, ILogger logger)
    {
        _game = game;
        this.PlayerBusinessRules = PlayerBusinessRules;
        _playerLoader = playerLoader;
        _logger = logger;
    }

    public override async void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (Guard.AnyNull(connection, message)) return;

        if (!_game.CreatureManager.TryGetPlayer(connection.CreatureId, out var player)) return;

        var addVipPacket = new AddVipPacket(message);

        if (addVipPacket.Name?.Length > 20) return;

        var vipPlayer = await GetVipPlayer(addVipPacket);

        if (vipPlayer is null)
        {
            connection.Send(new TextMessagePacket("A player with this name does not exist.",
                TextMessageOutgoingType.Small));
            return;
        }

        _game.Dispatcher.AddEvent(new Event(() => player.Vip.AddToVip(vipPlayer)));
    }

    private async Task<IPlayer> GetVipPlayer(AddVipPacket addVipPacket)
    {
        if (Guard.IsNull(addVipPacket)) return null;

        //return player if it is already loaded in the game
        if (_game.CreatureManager.TryGetPlayer(addVipPacket.Name, out var vipPlayer)) return vipPlayer;

        var playerRecord = await GetPlayerRecord(addVipPacket);
        if (playerRecord is null) return null;

        if (_playerLoader.FirstOrDefault(x => x.IsApplicable(playerRecord)) is not { } playerLoader)
            return null;

        vipPlayer = playerLoader.Load(playerRecord);

        return vipPlayer;
    }

    private async Task<Server.Entities.Character> GetPlayerRecord(AddVipPacket addVipPacket)
    {
        Server.Entities.Character playerRecord = null;

        try
        {
            playerRecord = await PlayerBusinessRules.GetPlayer(addVipPacket.Name);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Unable to retrieve player record");
        }

        return playerRecord;
    }
}