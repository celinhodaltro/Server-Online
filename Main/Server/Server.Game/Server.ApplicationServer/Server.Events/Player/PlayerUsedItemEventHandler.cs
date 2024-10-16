using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.Items.Types.Usable;
using Server.Entities.Common.Creatures;
using Networking.Packets.Outgoing.Effect;
using Server.Common.Contracts;

namespace Server.Events.Player;

public class PlayerUsedItemEventHandler
{
    private readonly IGameServer game;

    public PlayerUsedItemEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(IPlayer player, IThing onThing, IUsableOn item)
    {
        if (item.Effect == EffectT.None) return;

        foreach (var spectator in game.Map.GetPlayersAtPositionZone(onThing.Location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            connection.OutgoingPackets.Enqueue(new MagicEffectPacket(onThing.Location, item.Effect));
            connection.Send();
        }
    }
}