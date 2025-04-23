using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterEventType
{
    internal static IEnumerable<BaseEvent> Perform(IEnumerable<BaseEvent> ary)
    {
        var id = Options.EventTypeDropdown.name == "Custom" ? Options.EventType.Operand1 : Options.EventTypeDropdown.id;
        return !Options.EventType.Enabled ? ary : ary.Where(obj => obj.Type == id);
    }
}