using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterNoteDirection
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseNote
    {
        if (!Options.GridDirection.Enabled) return ary;
        var id = Options.GridDirectionDropdown.name == "Custom"
            ? Options.GridDirection.Operand1
            : Options.GridDirectionDropdown.id;

        return Options.GridDirectionDropdown.name switch
        {
            "Unknown" => ary.Where(obj => obj.CutDirection < 0 || obj.CutDirection > 8),
            "ME" => ary.Where(obj =>
                obj.CutDirection >= 1000 && obj.CutDirection <= 1360),
            _ => ary.Where(obj => obj.CutDirection == id)
        };
    }
}