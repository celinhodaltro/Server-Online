﻿using System.Threading.Tasks;
using Data.Entities;
using Data.Interfaces;
using Server.Entities.Common.Results;
using Networking.Packets.Incoming;
using Networking.Packets.Outgoing;
using Server.BusinessRules;
using Server.Commands.Player;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;
using Server.Common.Enums;
using Server.Configurations;
using Server.Tasks;

namespace Networking.Handlers.LogIn;

public class PlayerLogInHandler : PacketHandler
{
    private readonly IAccountRepository _accountRepository;
    private readonly CharacterBusinessRules playerBusinessRules;
    private readonly IGameServer _game;
    private readonly PlayerLogInCommand _playerLogInCommand;
    private readonly PlayerLogOutCommand _playerLogOutCommand;
    private readonly ServerConfiguration _serverConfiguration;

    public PlayerLogInHandler(IAccountRepository repositoryNeo,
        IGameServer game, ServerConfiguration serverConfiguration, PlayerLogInCommand playerLogInCommand,
        PlayerLogOutCommand playerLogOutCommand)
    {
        _accountRepository = repositoryNeo;
        _game = game;
        _serverConfiguration = serverConfiguration;
        _playerLogInCommand = playerLogInCommand;
        _playerLogOutCommand = playerLogOutCommand;
    }

    public override void HandleMessage(IReadOnlyNetworkMessage message, IConnection connection)
    {
        if (_game.State == GameState.Stopped) connection.Close();

        var packet = new PlayerLogInPacket(message);

        connection.SetXtea(packet.Xtea);

        //todo linux os

        if (!Verify(connection, packet)) return;

        //todo: ip ban validation

        async void TryConnect()
        {
            await Connect(connection, packet);
        }

        _game.Dispatcher.AddEvent(new Event(TryConnect));
    }

    private async Task Connect(IConnection connection, PlayerLogInPacket packet)
    {
        var playerOnline = await _accountRepository.GetOnlinePlayer(packet.Account);

        if (ValidateOnlineStatus(connection, playerOnline, packet).Failed) return;

        var playerRecord = await playerBusinessRules.GetPlayer(packet.Account, packet.Password, packet.CharacterName);

        if (playerRecord is null)
        {
            Disconnect(connection, "Account name or password is not correct.");
            return;
        }

        if (playerRecord.Account.UserInfo.BanishedAt is not null)
        {
            Disconnect(connection, "Your account is banned.");
            return;
        }

        _game.Dispatcher.AddEvent(new Event(() => _playerLogInCommand.Execute(playerRecord, connection)));
    }

    private Result ValidateOnlineStatus(IConnection connection, PlayerEntity playerOnline,
        PlayerLogInPacket packet)
    {
        if (playerOnline is null) return Result.Success;

        _game.CreatureManager.TryGetLoggedPlayer((uint)playerOnline.Id, out var player);

        if (player?.Name == packet.CharacterName)
        {
            _playerLogOutCommand.Execute(player, true);
            return Result.Success;
        }

        if (playerOnline.Account.AllowManyOnline) return Result.Success;

        Disconnect(connection, "You may only login with one character of your account at the same time.");
        return Result.NotPossible;
    }

    private bool Verify(IConnection connection, PlayerLogInPacket packet)
    {
        if (string.IsNullOrWhiteSpace(packet.Account))
        {
            Disconnect(connection, "You must enter your account name.");
            return false;
        }

        if (_serverConfiguration.Version != packet.Version)
        {
            Disconnect(connection, $"Only clients with protocol {_serverConfiguration.Version} allowed!");
            return false;
        }

        switch (_game.State)
        {
            case GameState.Opening:
                Disconnect(connection, "Gameworld is starting up. Please wait.");
                return false;
            case GameState.Maintaining:
                Disconnect(connection, "Gameworld is under maintenance. Please re-connect in a while.");
                return false;
            case GameState.Closed:
                Disconnect(connection, "Server is currently closed.\nPlease try again later.");
                return false;
        }

        return true;
    }

    private static void Disconnect(IConnection connection, string message)
    {
        connection.Send(new GameServerDisconnectPacket(message));
        connection.Close();
    }
}