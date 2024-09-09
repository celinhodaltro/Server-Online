using Server.Entities.Contracts.Creatures;

namespace Server.Entities.Contracts.Combat;

public interface ICombatDefense : IProbability
{
    ushort Interval { get; init; }

    void Defend(ICombatActor actor);
}