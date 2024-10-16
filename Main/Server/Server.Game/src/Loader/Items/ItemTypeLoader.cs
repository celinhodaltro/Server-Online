using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using Data.InMemory;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Item;
using Loader.Items.Parsers;
using Loader.OTB.Parsers;
using Loader.OTB.Structure;
using Server.Configurations;
using Server.Helpers.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace Loader.Items;

public class ItemTypeLoader
{
    private readonly IItemClientServerIdMapStore _itemClientServerIdMapStore;

    private readonly IItemTypeStore _itemTypeStore;
    private readonly ILogger _logger;
    private readonly ServerConfiguration _serverConfiguration;

    public ItemTypeLoader(ILogger logger, ServerConfiguration serverConfiguration, IItemTypeStore itemTypeStore,
        IItemClientServerIdMapStore itemClientServerIdMapStore)
    {
        _logger = logger;
        _serverConfiguration = serverConfiguration;
        _itemTypeStore = itemTypeStore;
        _itemClientServerIdMapStore = itemClientServerIdMapStore;
    }

    /// <summary>
    ///     Loads the OTB and XML files into a collection of ItemType objects
    /// </summary>
    public void Load()
    {
        _logger.Step("Loading items", "{n} items loaded", () =>
        {
            var basePath = $"{_serverConfiguration.Data}/items/";
            var itemTypes = LoadOtb(basePath);

            LoadItemsJson(basePath, itemTypes);

            foreach (var item in itemTypes.OrderBy(x => x.Key))
            {
                _itemTypeStore.Add(item.Key, item.Value);
                _itemClientServerIdMapStore.Add(item.Value.ClientId, item.Key);

                if (item.Value.Attributes.GetAttribute(ItemAttribute.Type)
                        ?.Equals("coin", StringComparison.InvariantCultureIgnoreCase) ??
                    false) CoinTypeStore.Data.Add(item.Key, item.Value);
            }

            return new object[] { itemTypes.Count };
        });
    }

    private Dictionary<ushort, IItemType> LoadOtb(string basePath)
    {
        var fileStream = File.ReadAllBytes(Path.Combine(basePath, _serverConfiguration.OTB));

        var otbNode = OtbBinaryTreeBuilder.Deserialize(fileStream);
        var otb = new Otb(otbNode);

        var itemTypes = otb.ItemNodes.AsParallel().Select(ItemNodeParser.Parse).ToDictionary(x => x.TypeId);
        return itemTypes;
    }

    private static void LoadItemsJson(string basePath, IDictionary<ushort, IItemType> itemTypes)
    {
        var itemTypeMetadata = GetItemTypeMetadataList(basePath);

        var itemTypeMetadataParser = new ItemTypeMetadataParser(itemTypes);

        (itemTypeMetadata ?? Array.Empty<ItemTypeMetadata>()).AsParallel().ForAll(metadata =>
        {
            if (metadata.Id.HasValue)
            {
                itemTypeMetadataParser.AddMetadata(metadata, metadata.Id.Value);
                return;
            }

            if (metadata.Fromid == null)
            {
                Console.WriteLine("No item id found");
                return;
            }

            if (metadata.Toid == null)
            {
                Console.WriteLine($"fromid ({metadata.Fromid}) without toid");
                return;
            }

            var id = metadata.Fromid.Value;
            while (id <= metadata.Toid) itemTypeMetadataParser.AddMetadata(metadata, id++);
        });
    }

    private static IEnumerable<ItemTypeMetadata> GetItemTypeMetadataList(string basePath)
    {
        using var memoryMappedFile = MemoryMappedFile.CreateFromFile(Path.Combine(basePath, "items.json"));
        using var stream = memoryMappedFile.CreateViewStream();
        using var reader = new StreamReader(stream);
        using var jsonReader = new JsonTextReader(reader) { CloseInput = false };

        var serializer = JsonSerializer.Create();
        return serializer.Deserialize<IEnumerable<ItemTypeMetadata>>(jsonReader);
    }
}