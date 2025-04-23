using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterGridY
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseGrid
    {
        return !Options.GridYSelect ? ary : ary.Where(o => o.PosY == Options.GridY);
    }
}