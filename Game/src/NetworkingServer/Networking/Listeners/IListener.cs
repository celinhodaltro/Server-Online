using System.Threading;

namespace Networking.Listeners;

internal interface IListener
{
    void BeginListening(CancellationToken cancellationToken);
    void EndListening();
}