namespace Game.Common.Contracts.World.Tiles;

public interface IStaticTile : ITile
{
    byte[] Raw { get; }
    ushort[] AllClientIdItems { get; }
}