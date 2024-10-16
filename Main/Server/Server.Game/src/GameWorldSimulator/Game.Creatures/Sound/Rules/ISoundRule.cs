﻿using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts.Creatures;

namespace Game.Creatures.Sound.Rules;

internal interface ISoundRule
{
    bool IsApplicable(SpeechType type);
    bool IsSatisfied(ICreature creature, SpeechType type, ICreature spectator);
}