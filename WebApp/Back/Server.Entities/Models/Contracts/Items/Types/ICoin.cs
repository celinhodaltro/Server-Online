namespace Server.Entities.Models.Contracts.Items.Types;

public interface ICoin : ICumulative
{
    uint Worth { get; }
}