using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Items.Types;

public delegate void CreatureWalkedThroughGround(ICreature creature, IGround ground);

public interface IGround : IItem, IHasDecay
{
    public ushort StepSpeed { get; }
    byte MovementPenalty { get; }
    void CreatureEntered(ICreature creature);
    event CreatureWalkedThroughGround OnCreatureWalkedThrough;
}