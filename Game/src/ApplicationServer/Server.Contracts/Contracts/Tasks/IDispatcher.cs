using System.Threading;

namespace Server.Common.Contracts.Tasks;

public interface IDispatcher
{
    void AddEvent(IEvent evt);

    void Start(CancellationToken token);
}