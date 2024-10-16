using Server.Entities.Common.Chats;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Game.Creatures.Sound;

namespace Game.Creatures.Events;

public class CreatureSayEventHandler : IGameEventHandler
{
    private readonly IMap map;

    public CreatureSayEventHandler(IMap map)
    {
        this.map = map;
    }

    public void Execute(ICreature creature, SpeechType speechType, string message, ICreature receiver = null)
    {
        if (creature is null) return;

        if (receiver is ISociableCreature sociableCreature)
        {
            sociableCreature.Hear(creature, speechType, message);
            return;
        }

        foreach (var spectator in map.GetCreaturesAtPositionZone(creature.Location))
        {
            if (!SoundRuleValidator.ShouldHear(creature, spectator, speechType)) continue;

            if (spectator is ISociableCreature listener)
                listener.Hear(creature, speechType, message);
        }
    }
}