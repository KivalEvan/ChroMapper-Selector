using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterSliderColor
{
    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseSlider
    {
        var id = Options.GridColorDropdown.name == "Custom" ? Options.GridColor.Operand1 : Options.GridColorDropdown.id;
        return !Options.GridColor.Enabled ? ary : ary.Where(obj => obj.Color == id);
    }
}