using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Creature;

public class CreatureLightPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public CreatureLightPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.CreatureLight);

        message.AddUInt32(player.CreatureId);
        message.AddByte(player.LightBrightness); // light level
        message.AddByte(player.LightColor); // color
    }
}