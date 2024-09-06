using Extensions.Chat;
using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;
using Game.Common.Services;
using Game.Creatures.Monster.Summon;

namespace Extensions.Events.Creatures;

public class CreatureDroppedLootEventHandler : IGameEventHandler
{
    public void Execute(ICombatActor killed, IThing by, ILoot loot)
    {
        if (killed is Summon) return;
        if (loot?.Owners is null) return;

        foreach (var owner in loot.Owners)
        {
            if (owner is not IPlayer player) continue;

            if (player.Channels.PersonalChannels is null) continue;

            var lootContentText = killed?.Corpse?.ToString() ?? "nothing";

            var lootText = $"Loot of a {killed?.Name.ToLower()}: {lootContentText}.";

            SendToLootChannel(killed, player, lootText);
            NotificationSenderService.Send(player, lootText);
        }
    }

    private static void SendToLootChannel(ICombatActor killed, IPlayer player, string lootText)
    {
        foreach (var channel in player.Channels.PersonalChannels)
        {
            if (channel is not LootChannel lootChannel) continue;

            lootChannel.WriteMessage(lootText,
                out _);
        }
    }
}