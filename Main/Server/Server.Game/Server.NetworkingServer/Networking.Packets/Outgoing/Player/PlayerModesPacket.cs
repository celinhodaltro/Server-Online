using Server.Entities.Common.Contracts.Creatures;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Player;

public class PlayerModesPacket : OutgoingPacket
{
    private readonly IPlayer player;

    public PlayerModesPacket(IPlayer player)
    {
        this.player = player;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.PlayerModes);
        message.AddByte((byte)player.FightMode);
        message.AddByte((byte)player.ChaseMode);
        message.AddByte(player.SecureMode);
    }
}