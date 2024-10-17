using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Combat;

public interface ICombatDefense : IProbability
{
    ushort Interval { get; init; }

    void Defend(ICombatActor actor);
}