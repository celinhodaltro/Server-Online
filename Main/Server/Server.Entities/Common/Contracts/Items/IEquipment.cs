using System;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types;

namespace Server.Entities.Common.Contracts.Items;

public interface IEquipment : IDecay, ISkillBonus, IDressable, IProtection, ITransformableEquipment, IChargeable,
    IHasDecay,
    IEquipmentRequirement
{
    IPlayer PlayerDressing { get; }
    event Action<IEquipment> OnDressed;
    event Action<IEquipment> OnUndressed;
}