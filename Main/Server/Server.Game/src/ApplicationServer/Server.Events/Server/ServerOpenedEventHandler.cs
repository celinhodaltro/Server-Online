﻿using Data.Interfaces;

namespace Server.Events.Server;

public class ServerOpenedEventHandler
{
    private readonly IPlayerRepository _playerRepository;

    public ServerOpenedEventHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public void Execute()
    {
        _playerRepository.UpdateAllPlayersToOffline();
    }
}