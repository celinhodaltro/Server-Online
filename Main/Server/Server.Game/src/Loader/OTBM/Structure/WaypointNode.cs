using Server.Entities.Common.Location.Structs;
using Loader.OTB.Parsers;
using Loader.OTB.Structure;

namespace Loader.OTBM.Structure;

public struct WaypointNode
{
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }

    public WaypointNode(OtbNode node)
    {
        var stream = new OtbParsingStream(node.Data);

        Name = stream.ReadString();

        Coordinate = stream.ReadCoordinate();
    }
}