using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Item;
using Game.Items;
using Loader.OTB.Parsers;
using Newtonsoft.Json.Linq;

namespace Loader.Items.Parsers;

public class ItemTypeMetadataParser
{
    private readonly IDictionary<ushort, IItemType> itemTypes;

    public ItemTypeMetadataParser(IDictionary<ushort, IItemType> itemTypes)
    {
        this.itemTypes = itemTypes;
    }

    /// <summary>
    ///     Parses ItemNode object to IItemType
    /// </summary>
    /// <returns></returns>
    public void AddMetadata(ItemTypeMetadata metadata, ushort itemTypeId)
    {
        var id = itemTypeId;

        if (id is > 30000 and < 30100) id -= 30000;

        if (!itemTypes.TryGetValue(id, out var itemType)) return;

        itemType.SetName(metadata.Name);
        itemType.SetArticle(metadata.Article);

        itemType.SetPlural(metadata.Plural);

        if (metadata.Flags is not null)
            foreach (var flagName in metadata.Flags)
            {
                if (!ItemAttributeTranslation.TranslateFlagName(flagName, out var flag)) continue;
                itemType.Flags.Add(flag);
            }

        if (metadata.Attributes == null)
        {
            itemType.SetGroupIfNone();
            return;
        }

        SetAttributes(metadata.Attributes, itemType.Attributes);

        if (metadata.OnUseEvent == null)
        {
            itemType.SetGroupIfNone();
            return;
        }

        foreach (var attribute in metadata.OnUseEvent)
        {
            var itemAttribute = ItemAttributeTranslation.Translate(attribute.Key, out _);
            itemType.SetOnUse();

            var value = AttributeValueParser.Parse(attribute.Value);

            if (itemAttribute == ItemAttribute.None)
                itemType.OnUse.SetCustomAttribute(attribute.Key, value);
            else
                itemType.OnUse.SetAttribute(itemAttribute, value);
        }

        itemType.SetGroupIfNone();
    }

    private static void SetAttributes(IEnumerable<ItemTypeMetadata.Attribute> metaAttributes,
        IItemAttributeList attributes)
    {
        foreach (var attribute in metaAttributes)
        {
            var itemAttribute = ItemAttributeTranslation.Translate(attribute.Key, out _);

            var value = itemAttribute == ItemAttribute.Weight
                ? (int.Parse(attribute.Value) / 100f).ToString(CultureInfo.InvariantCulture)
                : attribute.Value; //todo place this code in another place

            if (attribute.Attributes is null || !attribute.Attributes.Any())
            {
                if (value is JArray jArray)
                {
                    value = jArray.ToObject<string[]>();

                    if (itemAttribute == ItemAttribute.None)
                        attributes.SetCustomAttribute(attribute.Key, values: value);
                    else
                        attributes.SetAttribute(itemAttribute, values: value);
                }
                else
                {
                    if (itemAttribute == ItemAttribute.None)
                        attributes.SetCustomAttribute(attribute.Key, value);
                    else
                        attributes.SetAttribute(itemAttribute, value);
                }
            }
            else
            {
                var innerAttributes = new ItemAttributeList();

                SetAttributes(attribute.Attributes, innerAttributes);

                if (itemAttribute == ItemAttribute.None)
                    attributes.SetCustomAttribute(attribute.Key, value, innerAttributes);
                else
                    attributes.SetAttribute(itemAttribute, value, innerAttributes);
            }
        }
    }
}