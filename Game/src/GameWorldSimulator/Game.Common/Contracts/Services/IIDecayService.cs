using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.Services;

public interface IDecayService
{
    void Decay(IItem item);
}