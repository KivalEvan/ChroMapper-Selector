using System;
using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterEventFloatValue
{
    internal static IEnumerable<BaseEvent> Perform(IEnumerable<BaseEvent> ary)
    {
        if (!Options.EventFloatValueSelect) return ary;
        return ary.Where(obj =>
            Math.Abs(obj.FloatValue - Options.EventFloatValue) <= Options.EventFloatValueTolerance);
    }
}