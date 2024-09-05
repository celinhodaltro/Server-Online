using System;
using Game.Common.Contracts.Creatures;
using Game.Common.Results;

namespace Game.Common.Contracts.Items.Types;

public interface IReadable : IItem
{
    string Text { get; }
    bool CanWrite { get; }
    ushort MaxLength { get; }
    string WrittenBy { get; } //todo: change to id and then query the database to get the current name
    DateTime? WrittenOn { get; set; }
    Result Write(string text, IPlayer writtenBy);
}