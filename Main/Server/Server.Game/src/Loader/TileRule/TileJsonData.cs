﻿using Newtonsoft.Json;

namespace Loader.TileRule;

public class TileJsonData
{
    [JsonProperty("tiles")] public ushort[][] Locations { get; set; }

    public int MinLevel { get; set; }
    public int MaxLevel { get; set; }
    public bool RequiresPremium { get; set; }
    public string Message { get; set; }
}