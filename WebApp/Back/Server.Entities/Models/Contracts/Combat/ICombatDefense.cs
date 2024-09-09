using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Combat;

public interface ICombatDefense : IProbability
{
    ushort Interval { get; init; }

    void Defend(ICombatActor actor);
}