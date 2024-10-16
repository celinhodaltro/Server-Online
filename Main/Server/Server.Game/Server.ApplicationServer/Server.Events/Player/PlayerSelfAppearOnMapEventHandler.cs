using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Parsers;
using Networking.Packets.Outgoing.Creature;
using Networking.Packets.Outgoing.Effect;
using Networking.Packets.Outgoing.Map;
using Networking.Packets.Outgoing.Player;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;

namespace Server.Events.Player;

public class PlayerSelfAppearOnMapEventHandler : IEventHandler
{
    private readonly IGameServer game;
    private readonly IMap map;

    public PlayerSelfAppearOnMapEventHandler(IMap map, IGameServer game)
    {
        this.map = map;
        this.game = game;
    }

    public void Execute(IWalkableCreature creature)
    {
        if (creature.IsNull()) return;

        if (creature is not IPlayer player) return;

        if (!game.CreatureManager.GetPlayerConnection(creature.CreatureId, out var connection)) return;

        SendPacketsToPlayer(player, connection);
    }

    private void SendPacketsToPlayer(IPlayer player, IConnection connection)
    {
        connection.OutgoingPackets.Enqueue(new SelfAppearPacket(player));
        connection.OutgoingPackets.Enqueue(new MapDescriptionPacket(player, map));
        connection.OutgoingPackets.Enqueue(new MagicEffectPacket(player.Location, EffectT.BubbleBlue));
        connection.OutgoingPackets.Enqueue(new PlayerInventoryPacket(player.Inventory));
        connection.OutgoingPackets.Enqueue(new PlayerStatusPacket(player));
        connection.OutgoingPackets.Enqueue(new PlayerSkillsPacket(player));

        connection.OutgoingPackets.Enqueue(new WorldLightPacket(game.LightLevel, game.LightColor));

        connection.OutgoingPackets.Enqueue(new CreatureLightPacket(player));

        ushort icons = 0;
        foreach (var condition in player.Conditions) icons |= (ushort)ConditionIconParser.Parse(condition.Key);

        connection.OutgoingPackets.Enqueue(new ConditionIconPacket(icons));
    }
}