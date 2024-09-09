using System.Reflection;
using Server.Entities.Location;

namespace Server.Entities.Contracts.DataStores;

public interface IAreaEffectStore
{
    void Add(string name, FieldInfo area);
    byte[,] Get(string name);

    /// <summary>
    ///     Add area for each direction.
    /// </summary>
    /// <param name="name">Area Effect name</param>
    /// <param name="area">Reflection field</param>
    /// <param name="areas">Each direction area. Used when area effect is rotatable</param>
    void Add(string name, FieldInfo area, byte[][,] areas);

    byte[,] Get(string name, Direction direction);
}