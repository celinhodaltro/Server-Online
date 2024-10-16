using System.Collections.Generic;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Inspection;
using Server.Entities.Common.Contracts.Items;
using Networking.Packets.Outgoing;
using Server.Contracts.Contracts;

namespace Server.Events.Player;

public class PlayerLookedAtEventHandler
{
    private readonly IGameServer _game;
    private readonly IEnumerable<IInspectionTextBuilder> _inspectionTextBuilders;

    public PlayerLookedAtEventHandler(IGameServer game, IEnumerable<IInspectionTextBuilder> inspectionTextBuilders)
    {
        _game = game;
        _inspectionTextBuilders = inspectionTextBuilders;
    }

    public void Execute(IPlayer player, IThing thing, bool isClose)
    {
        if (_game.CreatureManager.GetPlayerConnection(player.CreatureId, out var connection) is false) return;

        var inspectionTextBuilder = GetInspectionTextBuilder(thing);

        var text = thing.GetLookText(inspectionTextBuilder, player, isClose);

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(text, TextMessageOutgoingType.Description));
        connection.Send();
    }

    private IInspectionTextBuilder GetInspectionTextBuilder(IThing thing)
    {
        foreach (var inspectionTextBuilder in _inspectionTextBuilders)
            if (inspectionTextBuilder.IsApplicable(thing))
                return inspectionTextBuilder;

        return null;
    }
}