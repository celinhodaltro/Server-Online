using System;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items;

namespace Game.Common.Contracts.Inspection;

public interface IInspectionTextBuilder
{
    string Build(IThing thing, IPlayer player, bool isClose = false);
    bool IsApplicable(IThing thing);

    public static string GetArticle(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return "a";

        Span<char> vowels = stackalloc char[5] { 'a', 'e', 'i', 'o', 'u' };
        return vowels.Contains(name.ToLower()[0]) ? "an" : "a";
    }
}