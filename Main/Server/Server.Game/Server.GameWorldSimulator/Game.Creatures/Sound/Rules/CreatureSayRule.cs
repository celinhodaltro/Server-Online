﻿using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Sound.Rules;

internal class CreatureSayRule : ISoundRule
{
    public bool IsApplicable(SpeechType type)
    {
        return type is SpeechType.Say or SpeechType.MonsterSay;
    }

    public bool IsSatisfied(ICreature creature, SpeechType type, ICreature spectator)
    {
        if (!creature.Location.SameFloorAs(spectator.Location)) return false;

        if (!creature.CanSee(spectator)) return false;

        return true;
    }
}