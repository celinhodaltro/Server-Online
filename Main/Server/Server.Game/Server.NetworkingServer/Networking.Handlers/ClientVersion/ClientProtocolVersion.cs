﻿using Server.Configurations;
using Serilog;

namespace Networking.Handlers.ClientVersion;

public class ClientProtocolVersion
{
    private readonly ILogger _logger;
    private readonly ServerConfiguration _serverConfiguration;

    public ClientProtocolVersion(ILogger logger, ServerConfiguration serverConfiguration)
    {
        _logger = logger;
        _serverConfiguration = serverConfiguration;
    }

    public bool IsSupported(uint version)
    {
        if (version == _serverConfiguration.Version) return true;

        _logger.Warning($"Client protocol version {version} is not supported");
        return false;
    }
}