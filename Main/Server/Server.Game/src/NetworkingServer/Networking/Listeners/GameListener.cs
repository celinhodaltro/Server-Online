using Networking.Protocols;
using Serilog;

namespace Networking.Listeners;

public class GameListener : Listener
{
    public GameListener(GameProtocol protocol, ILogger logger) : base(7172,
        protocol, logger)
    {
    }
}