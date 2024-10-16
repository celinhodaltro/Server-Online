using System.Linq;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Location;
using Server.Common.Contracts.Network;

namespace Networking.Packets.Outgoing.Map;

public class MapDescriptionPacket : OutgoingPacket
{
    private readonly IMap _map;
    private readonly IPlayer _player;

    public MapDescriptionPacket(IPlayer player, IMap map)
    {
        _player = player;
        _map = map;
    }

    public override void WriteToMessage(INetworkMessage message)
    {
        message.AddByte((int)GameOutgoingPacketType.MapDescription);
        message.AddLocation(_player.Location);

        message.AddBytes(GetMapDescription(_player, _map));
    }

    private static byte[] GetMapDescription(IPlayer player, IMap map)
    {
        var location = player.Location;
        return map.GetDescription(player, (ushort)(location.X - MapViewPort.MaxClientViewPortX),
            (ushort)(location.Y - MapViewPort.MaxClientViewPortY), location.Z).ToArray();
    }
}