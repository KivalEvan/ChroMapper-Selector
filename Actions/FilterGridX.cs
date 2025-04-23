using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterGridX
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseGrid
    {
        return !Options.GridX.Enabled ? ary : ary.Where(o => o.PosX == Options.GridX.Operand1);
    }
}