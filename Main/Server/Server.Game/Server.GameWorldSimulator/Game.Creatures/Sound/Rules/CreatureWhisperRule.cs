using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Sound.Rules;

internal class CreatureWhisperRule : ISoundRule
{
    public bool IsApplicable(SpeechType type)
    {
        return type is SpeechType.Whisper;
    }

    public bool IsSatisfied(ICreature creature, SpeechType type, ICreature spectator)
    {
        if (!creature.Location.SameFloorAs(spectator.Location)) return false;

        if (creature.Location.GetSqmDistance(spectator.Location) > 1) return false;

        return true;
    }
}