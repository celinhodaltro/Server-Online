using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Combat;

public interface ICombatDefense : IProbability
{
    ushort Interval { get; init; }

    void Defend(ICombatActor actor);
}