using Server.Entities.Common.Contracts.World;
using Server.Contracts.Contracts.Tasks;
using Server.Common.Enums;

namespace Server.Contracts.Contracts;

public delegate void OpenServer();

public interface IGameServer
{
    IGameCreatureManager CreatureManager { get; }
    IMap Map { get; }
    IDispatcher Dispatcher { get; }
    IScheduler Scheduler { get; }
    IDecayableItemManager DecayableItemManager { get; }
    GameState State { get; }
    byte LightLevel { get; }
    byte LightColor { get; }
    IPersistenceDispatcher PersistenceDispatcher { get; }
    void Close();
    void Open();
    event OpenServer OnOpened;
}