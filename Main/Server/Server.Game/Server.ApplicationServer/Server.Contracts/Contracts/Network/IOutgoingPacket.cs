namespace Server.Contracts.Contracts.Network;

public interface IOutgoingPacket
{
    void WriteToMessage(INetworkMessage message);
}