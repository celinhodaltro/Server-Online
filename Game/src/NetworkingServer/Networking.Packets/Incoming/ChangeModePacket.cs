using Game.Common.Creatures.Players;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Incoming;

public class ChangeModePacket : IncomingPacket
{
    public ChangeModePacket(IReadOnlyNetworkMessage message)
    {
        FightMode = (FightMode)message.GetByte();
        ChaseMode = (ChaseMode)message.GetByte();
        SecureMode = message.GetByte();
    }

    public FightMode FightMode { get; }
    public ChaseMode ChaseMode { get; }
    public byte SecureMode { get; }
}