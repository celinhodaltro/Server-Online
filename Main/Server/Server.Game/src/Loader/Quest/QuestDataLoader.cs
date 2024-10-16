﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Item;
using Server.Configurations;
using Server.Helpers.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace Loader.Quest;

public class QuestDataLoader
{
    private readonly ILogger _logger;
    private readonly IQuestDataStore _questDataStore;
    private readonly ServerConfiguration _serverConfiguration;

    public QuestDataLoader(ILogger logger,
        ServerConfiguration serverConfiguration, IQuestDataStore questDataStore)
    {
        _logger = logger;
        _serverConfiguration = serverConfiguration;
        _questDataStore = questDataStore;
    }

    public void Load()
    {
        _logger.Step("Loading quests...", "{n} quests loaded", () =>
        {
            _questDataStore.Clear();
            var actions = GetQuests();
            actions.ForEach(x => _questDataStore.Add(x.Key, x));

            return new object[] { actions.Count };
        });
    }

    private List<QuestData> GetQuests()
    {
        var basePath = $"{_serverConfiguration.Data}";
        var jsonString = File.ReadAllText(Path.Combine(basePath, "quests.json"));
        var quests = JsonConvert.DeserializeObject<List<QuestModel>>(jsonString,
            new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Populate });

        return quests?.Select(x => new QuestData
        {
            Script = x.Script,
            ActionId = x.ActionId,
            UniqueId = x.UniqueId,
            Rewards = MapRewards(x.Rewards),
            Name = x.Name,
            Group = x.Group,
            GroupKey = x.GroupKey,
            AutoLoad = x.AutoLoad
        }).ToList();
    }

    private static QuestData.Reward[] MapRewards(List<QuestModel.Reward> rewards)
    {
        if (rewards is null) return Array.Empty<QuestData.Reward>();
        return rewards.Select(r => new QuestData.Reward
        {
            Amount = r.Amount == 0 ? (byte)1 : r.Amount,
            Children = MapRewards(r.Children),
            ItemId = r.ItemId
        }).ToArray();
    }
}