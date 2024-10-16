﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Interfaces;
using Server.Entities.Common.Contracts.Creatures;
using Game.Systems.Depot;
using Server.Common.Contracts;
using Server.Configurations;
using Serilog;

namespace Server.Jobs.Persistence;

public class PlayerPersistenceJob
{
    private readonly DepotManager _depotManager;
    private readonly IGameServer _gameServer;
    private readonly ILogger _logger;
    private readonly IPlayerDepotItemRepository _playerDepotItemRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ServerConfiguration _serverConfiguration;
    private readonly Stopwatch _stopwatch = new();

    private int _saveInterval;

    public PlayerPersistenceJob(IGameServer gameServer, IPlayerRepository playerRepository, ILogger logger,
        ServerConfiguration serverConfiguration,
        IPlayerDepotItemRepository playerDepotItemRepository,
        DepotManager depotManager)
    {
        _gameServer = gameServer;
        _playerRepository = playerRepository;
        _logger = logger;
        _serverConfiguration = serverConfiguration;
        _playerDepotItemRepository = playerDepotItemRepository;
        _depotManager = depotManager;
    }

    public void Start(CancellationToken token)
    {
        _saveInterval = (int)(_serverConfiguration?.Save?.Players ?? (uint)_saveInterval);
        _saveInterval = (_saveInterval == 0 ? 3600 : _saveInterval) * 1000;
        Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                _gameServer.PersistenceDispatcher.AddEvent(async () => await SavePlayers());
                await Task.Delay(_saveInterval, token);
            }
        }, token);
    }

    private async Task SavePlayers()
    {
        var players = _gameServer.CreatureManager.GetAllLoggedPlayers().ToList();

        if (players.Any())
        {
            _logger.Information("Saving {NumPlayers} players...", players.Count);
            _stopwatch.Restart();

            await _playerRepository.UpdatePlayers(players);

            await SaveDepots(players);

            _logger.Information("{NumPlayers} players saved in {Elapsed} ms", players.Count,
                _stopwatch.ElapsedMilliseconds);
        }
    }

    private async Task SaveDepots(List<IPlayer> players)
    {
        var depotSaveTasks = new List<Task>();

        foreach (var player in players)
        {
            if (!_depotManager.Get(player.Id, out var depot)) continue;
            depotSaveTasks.Add(_playerDepotItemRepository.Save(player, depot));
        }

        await Task.WhenAll(depotSaveTasks);
    }
}