using System.Linq;
using Extensions.Chat;
using Game.Common.Chats;
using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.DataStores;
using Game.Common.Contracts.Items;

namespace Extensions.Events.Creatures;

public class CreatureKilledEventHandler : IGameEventHandler
{
    private readonly IChatChannelStore _chatChannelStore;

    public CreatureKilledEventHandler(IChatChannelStore chatChannelStore)
    {
        _chatChannelStore = chatChannelStore;
    }

    public void Execute(ICombatActor actor, IThing by, ILoot loot)
    {
        AddDeathMessageToChannel(actor, by);
    }

    private void AddDeathMessageToChannel(ICombatActor actor, IThing by)
    {
        if (_chatChannelStore.All.FirstOrDefault(x => x is DeathChannel) is not { } deathChannel)
            return;
        if (actor is not IPlayer player) return;

        var message = $"{actor.Name} was KILLED at level {player.Level} by {by.Name}";
        deathChannel.WriteMessage(message, out var cancelMessage, SpeechType.ChannelOrangeText);
    }
}