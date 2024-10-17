using System.Collections.Generic;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items;

public interface IItemType
{
    ushort TypeId { get; }

    string Name { get; }
    string FullName { get; }
    string PluralName => Plural ?? $"{Name}s";

    string Description { get; }

    ISet<ItemFlag> Flags { get; }

    ItemGroup Group { get; }

    ushort ClientId { get; }

    ushort Speed { get; }
    string Article { get; }
    IItemAttributeList Attributes { get; }
    ShootType ShootType { get; }
    AmmoType AmmoType { get; }
    WeaponType WeaponType { get; }
    Slot BodyPosition { get; }
    float Weight { get; }
    ushort TransformTo { get; }
    string Plural { get; }
    IItemAttributeList OnUse { get; }
    DamageType DamageType { get; }
    EffectT EffectT { get; }
    void SetArticle(string article);
    void SetPlural(string plural);

    void SetName(string value);
    bool HasFlag(ItemFlag flag);
    void SetOnUse();
    bool HasAtLeastOneFlag(params ItemFlag[] flags);
    void SetGroupIfNone();
}