using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterCustomData
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseObject
    {
        if (Options.CustomDataSelect == CustomDataSelectType.Any) return ary;
        return ary.Where(obj =>
            Options.CustomDataSelect == CustomDataSelectType.None
                ? obj.CustomData.Count == 0
                : obj.CustomData.Count > 0);
    }
}