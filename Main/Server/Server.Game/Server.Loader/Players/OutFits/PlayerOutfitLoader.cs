﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Creatures.Players;
using Loader.Interfaces;
using Server.Configurations;
using Newtonsoft.Json;
using Serilog;
using Server.Entities.Common.Characters;

namespace Loader.Players.OutFits;

public class PlayerOutfitLoader : IStartupLoader
{
    private readonly ILogger _logger;
    private readonly IPlayerOutFitStore _playerOutFitStore;
    private readonly ServerConfiguration _serverConfiguration;

    public PlayerOutfitLoader(ServerConfiguration serverConfiguration, ILogger logger,
        IPlayerOutFitStore playerOutFitStore)
    {
        _serverConfiguration = serverConfiguration;
        _logger = logger;
        _playerOutFitStore = playerOutFitStore;
    }

    public void Load()
    {
        var path = $"{_serverConfiguration.Data}/player/outfits.json";

        if (!File.Exists(path))
        {
            _logger.Error("{Path} file not found", path);
            return;
        }

        var jsonContent = File.ReadAllText(path);
        var outfitsData = JsonConvert.DeserializeObject<IEnumerable<PlayerOutFitData>>(jsonContent).ToList();

        _playerOutFitStore.Add(Gender.Female, outfitsData.Where(item => item.Type == Gender.Female));
        _playerOutFitStore.Add(Gender.Male, outfitsData.Where(item => item.Type == Gender.Male));
    }
}