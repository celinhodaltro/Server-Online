using Server.Entities.Common.Chats;

namespace Server.Entities.Common.Creatures;

public readonly struct Voice
{
    public Voice(string sentence, SpeechType speechType)
    {
        Sentence = sentence;
        SpeechType = speechType;
    }

    public string Sentence { get; }
    public SpeechType SpeechType { get; }
}