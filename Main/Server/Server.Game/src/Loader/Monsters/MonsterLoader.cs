﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Configurations;
using Server.Helpers.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace Loader.Monsters;

public class MonsterLoader
{
    private readonly GameConfiguration _gameConfiguration;
    private readonly IItemTypeStore _itemTypeStore;
    private readonly ILogger _logger;
    private readonly IMonsterDataManager _monsterManager;
    private readonly ServerConfiguration _serverConfiguration;

    public MonsterLoader(IMonsterDataManager monsterManager, GameConfiguration gameConfiguration, ILogger logger,
        ServerConfiguration serverConfiguration, IItemTypeStore itemTypeStore)
    {
        _monsterManager = monsterManager;
        _gameConfiguration = gameConfiguration;
        _logger = logger;
        _serverConfiguration = serverConfiguration;
        _itemTypeStore = itemTypeStore;
    }

    public void Load()
    {
        _logger.Step("Loading monsters...", "{n} monsters loaded", () =>
        {
            var monsters = GetMonsterDataList().ToList();
            _monsterManager.Load(monsters);
            return new object[] { monsters.Count };
        });
    }

    private IEnumerable<(string, IMonsterType)> GetMonsterDataList()
    {
        var basePath = $"{_serverConfiguration.Data}/monsters";
        var jsonString = File.ReadAllText(Path.Combine(basePath, "monsters.json"));
        var monstersPath = JsonConvert.DeserializeObject<List<IDictionary<string, string>>>(jsonString);

        return monstersPath.AsParallel().Select(x => (x["name"], ConvertMonster(basePath, x)));
    }

    private IMonsterType ConvertMonster(string basePath, IDictionary<string, string> monsterFile)
    {
        var json = File.ReadAllText(Path.Combine(basePath, monsterFile["file"]));

        var monster = JsonConvert.DeserializeObject<MonsterData>(json, new JsonSerializerSettings
        {
            Error = (_, ev) =>
            {
                ev.ErrorContext.Handled = true;
                Console.WriteLine(ev.ErrorContext.Error);
            }
        });

        return MonsterConverter.Convert(monster, _gameConfiguration, _monsterManager, _logger, _itemTypeStore);
    }
}