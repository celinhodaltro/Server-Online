namespace Server.Entities.Common.Contracts.Items;

public interface IMovableThing : IThing
{
    void OnMoved(IThing to);
}