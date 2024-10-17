using System.Threading;

namespace Server.Contracts.Contracts.Tasks;

public interface IDispatcher
{
    void AddEvent(IEvent evt);

    void Start(CancellationToken token);
}