namespace Server.Entities.Common.Contracts.Items.Types;

public interface ICoin : ICumulative
{
    uint Worth { get; }
}