using Server.Entities.Common.Creatures.Players;
using Server.Contracts.Contracts.Network;
using Server.Entities.Common.Characters;

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