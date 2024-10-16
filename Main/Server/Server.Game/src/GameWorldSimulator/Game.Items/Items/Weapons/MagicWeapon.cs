using Game.Combat.Attacks;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Body;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Server.Entities.Common.Parsers;
using Game.Items.Bases;

namespace Game.Items.Items.Weapons;

public class MagicWeapon : Equipment, IDistanceWeapon
{
    public MagicWeapon(IItemType type, Location location) : base(type, location)
    {
    }

    private ShootType ShootType => Metadata.ShootType;

    private DamageType DamageType => Metadata.Attributes.HasAttribute(ItemAttribute.Damage)
        ? Metadata.DamageType
        : ShootTypeParser.ToDamageType(ShootType);

    private ushort MaxDamage => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.MaxHitChance);
    public ushort MinHitChance => (ushort)(MaxDamage / 2);
    private ushort ManaConsumption => Metadata.Attributes?.GetAttribute<ushort>(ItemAttribute.ManaUse) ?? 0;

    protected override string PartialInspectionText => string.Empty;
    public byte Range => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Range);
    public WeaponType WeaponType => WeaponType.Magical;

    public bool Attack(ICombatActor actor, ICombatActor enemy, out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult(ShootType);

        if (actor is not IPlayer player) return false;
        if (!player.HasEnoughMana(ManaConsumption)) return false;

        var combat = new CombatAttackValue((ushort)(MaxDamage / 2), MaxDamage, Range, DamageType);

        if (DistanceCombatAttack.CalculateAttack(actor, enemy, combat, out var damage))
        {
            player.ConsumeMana(ManaConsumption);
            enemy.ReceiveAttack(actor, damage);
            return true;
        }

        return false;
    }

    public override bool CanBeDressed(IPlayer player)
    {
        if (Guard.IsNullOrEmpty(Vocations)) return true;

        foreach (var vocation in Vocations)
            if (vocation == player.VocationType)
                return true;

        return false;
    }

    public void OnMoved(IThing to)
    {
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.MagicWeapon;
    }
}