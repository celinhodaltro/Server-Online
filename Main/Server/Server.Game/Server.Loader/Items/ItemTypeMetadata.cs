using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Loader.Items;

[Serializable]
public struct ItemTypeMetadata
{
    public ushort? Id { get; set; }
    public string Name { get; set; }
    public ushort? Fromid { get; set; }
    public ushort? Toid { get; set; }
    public IEnumerable<Attribute> Attributes { get; set; }

    [JsonProperty("onUse")] public IEnumerable<Attribute> OnUseEvent { get; set; }

    public string Article { get; set; }
    public string Plural { get; set; }
    public string Editorsuffix { get; set; }
    public Requirement[] Requirements { get; set; }
    public string[] Flags { get; set; }

    [Serializable]
    public struct Attribute
    {
        public string Key { get; set; }
        public dynamic Value { get; set; }
        public IEnumerable<Attribute> Attributes { get; set; }
    }

    [Serializable]
    public struct Requirement
    {
        public string Vocation { get; set; }
        public ushort MinLevel { get; set; }
    }
}