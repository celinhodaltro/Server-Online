using System.Linq;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Effects.Parsers;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Item;
using Server.Entities.Common.Location.Structs;
using Networking.Packets.Outgoing.Effect;
using Server.Contracts.Contracts;
using Server.Contracts.Contracts.Network;

namespace Server.Events.Combat;

public class CreatureAttackEventHandler
{
    private readonly IGameServer game;

    public CreatureAttackEventHandler(IGameServer game)
    {
        this.game = game;
    }

    public void Execute(ICreature creature, ICreature victim, CombatAttackResult[] attacks)
    {
        var spectators = game.Map.GetPlayersAtPositionZone(creature.Location);

        foreach (var spectator in spectators)
        {
            if (spectator is not IPlayer) continue;

            if (!game.CreatureManager.GetPlayerConnection(spectator.CreatureId, out var connection)) continue;

            SendAttack(creature, victim, attacks, connection);

            connection.Send();
        }
    }

    private static void SendAttack(ICreature creature, ICreature victim, CombatAttackResult[] attacks,
        IConnection connection)
    {
        foreach (var attack in attacks)
        {
            if (attack.Missed)
            {
                SendMissedAttack(creature, victim, attack, connection);
            }
            else
            {
                if (attack.ShootType != default && victim?.Location is not null)
                    connection.OutgoingPackets.Enqueue(new DistanceEffectPacket(creature.Location, victim.Location,
                        (byte)attack.ShootType));
            }

            if (attack.Area?.Any() ?? false)
                SpreadAreaEffect(attack, connection);

            else if (!attack.Missed && victim is not null) SendEffect(attack, connection, victim.Location);
        }
    }

    private static void SpreadAreaEffect(CombatAttackResult attack, IConnection connection)
    {
        foreach (var coordinate in attack.Area)
        {
            if (coordinate.Missed) continue;
            SendEffect(attack, connection, coordinate.Point.Location);
        }
    }

    private static void SendEffect(CombatAttackResult attack, IConnection connection, Location location)
    {
        attack.EffectT = attack.EffectT == 0 ? EffectT.None : attack.EffectT;
        var effect = attack.EffectT == EffectT.None ? DamageEffectParser.Parse(attack.DamageType) : attack.EffectT;

        if (attack.EffectT == EffectT.None && attack.DamageType == DamageType.Melee) return;
        if (effect == EffectT.None) return;

        connection.OutgoingPackets.Enqueue(new MagicEffectPacket(location, effect));
    }

    private static void SendMissedAttack(ICreature creature, ICreature victim, CombatAttackResult attack,
        IConnection connection)
    {
        Location destLocation;
        do
        {
            var index = GameRandom.Random.Next(0, maxValue: victim.Location.Neighbours.Length);
            destLocation = victim.Location.Neighbours[index];
        } while (destLocation == creature.Location);

        if (attack.ShootType != default)
            connection.OutgoingPackets.Enqueue(new DistanceEffectPacket(creature.Location, destLocation,
                (byte)attack.ShootType));
        connection.OutgoingPackets.Enqueue(new MagicEffectPacket(destLocation, EffectT.Puff));
    }
}