﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Texts;
using Networking.Packets.Outgoing;
using Networking.Packets.Outgoing.Effect;
using Networking.Packets.Outgoing.Player;
using Server.Common.Contracts;
using Server.Common.Contracts.Network;

namespace Server.Events.Player;

public class PlayerGainedExperienceEventHandler
{
    private readonly IGameServer game;

    public PlayerGainedExperienceEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature player, long experience)
    {
        var experienceText = experience.ToString();
        foreach (var spectator in game.Map.GetPlayersAtPositionZone(player.Location))
        {
            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            connection.OutgoingPackets.Enqueue(new AnimatedTextPacket(player.Location, TextColor.White,
                experienceText));

            TrySendMessageToYourself(player, spectator, connection, experienceText);
            TrySendMessageToSpectator(player, spectator, connection, experienceText);

            connection.Send();
        }
    }

    private static void TrySendMessageToSpectator(ICreature player, ICreature spectator, IConnection connection,
        string experienceText)
    {
        if (Equals(spectator, player)) return;

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(
            $"{player.Name} gained {experienceText} experience points.",
            TextMessageOutgoingType.MESSAGE_STATUS_DEFAULT));
    }

    private static void TrySendMessageToYourself(ICreature player, ICreature spectator, IConnection connection,
        string experienceText)
    {
        if (!Equals(spectator, player)) return;

        connection.OutgoingPackets.Enqueue(new TextMessagePacket(
            $"You gained {experienceText} experience points.",
            TextMessageOutgoingType.MESSAGE_STATUS_DEFAULT));

        connection.OutgoingPackets.Enqueue(new PlayerStatusPacket((IPlayer)player));
    }
}