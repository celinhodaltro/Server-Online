﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Configurations;
using Server.Helpers.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace Loader.Spawns;

public class SpawnLoader
{
    private readonly ILogger _logger;
    private readonly ServerConfiguration _serverConfiguration;
    private readonly Game.World.World _world;

    public SpawnLoader(Game.World.World world, ServerConfiguration serverConfiguration, ILogger logger)
    {
        _world = world;
        _serverConfiguration = serverConfiguration;
        _logger = logger;
    }

    public void Load()
    {
        _logger.Step("Loading spawns...", "{n} spawns loaded", () =>
        {
            var spawnData = GetSpawnData();

            var spawns = spawnData.AsParallel().Select(SpawnConverter.Convert).ToList();

            _world.LoadSpawns(spawns);
            return new object[] { spawns.Count };
        });
    }

    private IEnumerable<SpawnData> GetSpawnData()
    {
        var basePath = $"{_serverConfiguration.Data}/world/";

        var spawnPath = Path.Combine(basePath, $"{_serverConfiguration.OTBM.Replace(".otbm", "-spawn")}.json");
        if (!File.Exists(spawnPath)) return new List<SpawnData>();
        var jsonString = File.ReadAllText(spawnPath);
        var spawn = JsonConvert.DeserializeObject<IEnumerable<SpawnData>>(jsonString);
        return spawn;
    }
}