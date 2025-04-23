using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterNoteColor
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseNote
    {
        return !Options.GridColorSelect ? ary : ary.Where(obj => obj.Color == Options.GridColor.id);
    }
}