using System;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types;

namespace Server.Entities.Models.Contracts.Items;

public interface IEquipment : IDecay, ISkillBonus, IDressable, IProtection, ITransformableEquipment, IChargeable,
    IHasDecay,
    IEquipmentRequirement
{
    IPlayer PlayerDressing { get; }
    event Action<IEquipment> OnDressed;
    event Action<IEquipment> OnUndressed;
}