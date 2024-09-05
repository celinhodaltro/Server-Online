using System;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types;

namespace Game.Common.Contracts.Items;

public interface IEquipment : IDecay, ISkillBonus, IDressable, IProtection, ITransformableEquipment, IChargeable,
    IHasDecay,
    IEquipmentRequirement
{
    IPlayer PlayerDressing { get; }
    event Action<IEquipment> OnDressed;
    event Action<IEquipment> OnUndressed;
}