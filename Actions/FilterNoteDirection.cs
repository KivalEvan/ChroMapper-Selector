using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterNoteDirection
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseNote
    {
        if (!Options.GridDirectionSelect) return ary;
        var direction = Options.GridDirection.id;

        return Options.GridDirection.name switch
        {
            "Unknown" => ary.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8),
            "ME" => ary.Where(obj => obj is BaseSlider
                ? (obj.CutDirection >= 1000 && obj.CutDirection <= 1360) ||
                  (obj.CutDirection >= 2000 && obj.CutDirection <= 2360)
                : obj.CutDirection >= 1000 && obj.CutDirection <= 1360),
            _ => ary.Where(obj => obj.CutDirection == direction)
        };
    }
}