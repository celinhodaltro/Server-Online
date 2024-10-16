using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Contracts.Contracts.Tasks;

public interface IPersistenceDispatcher
{
    void AddEvent(Func<Task> evt);

    void Start(CancellationToken token);
}