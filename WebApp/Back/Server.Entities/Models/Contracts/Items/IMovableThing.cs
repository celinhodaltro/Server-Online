namespace Server.Entities.Models.Contracts.Items;

public interface IMovableThing : IThing
{
    void OnMoved(IThing to);
}