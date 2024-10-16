using Server.Entities.Common.Creatures;
using Server.Entities.Common.Location.Structs;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Effect;

public class MagicEffectPacket : OutgoingPacket
{
    private readonly EffectT effect;
    private readonly Location location;

    public MagicEffectPacket(Location location, EffectT effect)
    {
        this.location = location;
        this.effect = effect;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((byte)GameOutgoingPacketType.MagicEffect);
        message.AddLocation(location);
        message.AddByte((byte)effect);
    }
}