using Server.Entities.Models.Chats;

namespace Server.Entities.Models.Creatures;

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