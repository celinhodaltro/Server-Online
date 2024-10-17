using System.Threading;

namespace Server.Contracts.Contracts.Tasks;

public interface IScheduler
{
    void Start(CancellationToken token);

    uint AddEvent(ISchedulerEvent evt);
    bool CancelEvent(uint eventId);
    bool EventIsCancelled(uint eventId);
}