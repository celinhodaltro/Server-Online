﻿using System.Linq;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;

namespace Game.Creatures.Monster.Actions;

internal static class MonsterYell
{
    public static void Yell(Monster monster)
    {
        var metadata = monster.Metadata;

        if (metadata.Voices is null) return;
        if (metadata.VoiceConfig is null) return;
        if (!metadata.Voices.Any()) return;

        if (!monster.Cooldowns.Expired(CooldownType.Yell)) return;
        monster.Cooldowns.Start(CooldownType.Yell, monster.Metadata.VoiceConfig.Interval);

        if (metadata.VoiceConfig.Chance < GameRandom.Random.Next(1, maxValue: 100)) return;

        var voiceIndex = GameRandom.Random.Next(0, maxValue: metadata.Voices.Length - 1);

        var voice = metadata.Voices[voiceIndex];
        monster.Say(voice.Sentence, voice.SpeechType);
    }
}