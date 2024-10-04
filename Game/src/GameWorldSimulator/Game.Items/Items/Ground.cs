using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Game.Items.Bases;

namespace Game.Items.Items;

public class Ground : Item, IGround
{
    public Ground(IItemType type, Location location) : base(type, location)
    {
    }

    public event CreatureWalkedThroughGround OnCreatureWalkedThrough;
    public ushort StepSpeed => (Metadata?.Speed ?? 0) != 0 ? Metadata.Speed : (ushort)150;
    public byte MovementPenalty => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Waypoints);

    public void CreatureEntered(ICreature creature)
    {
        OnCreatureWalkedThrough?.Invoke(creature, this);
    }

    public static bool IsApplicable(IItemType type)
    {
        return type.Group is ItemGroup.Ground;
    }
}