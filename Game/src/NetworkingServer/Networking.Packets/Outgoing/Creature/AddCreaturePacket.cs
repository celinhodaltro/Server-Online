using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class AddCreaturePacket : OutgoingPacket
{
    private readonly IWalkableCreature creatureToAdd;
    private readonly IPlayer player;

    public AddCreaturePacket(IPlayer player, IWalkableCreature creatureToAdd)
    {
        this.creatureToAdd = creatureToAdd;
        this.player = player;
    }

    //todo: this code is duplicated?
    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddBytes(creatureToAdd.GetRaw(player));
    }
}