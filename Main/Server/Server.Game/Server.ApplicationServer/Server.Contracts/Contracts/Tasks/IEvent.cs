using System;

namespace Server.Common.Contracts.Tasks;

public interface IEvent
{
    Action Action { get; }

    bool HasExpired { get; }
    bool HasNoTimeout { get; }

    void SetToNotExpire();
}