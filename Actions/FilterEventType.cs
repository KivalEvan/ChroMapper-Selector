using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterEventType
{
    internal static IEnumerable<BaseEvent> Perform(IEnumerable<BaseEvent> ary)
    {
        if (!Options.EventTypeSelect) return ary;
        var type = Options.EventType.name switch
        {
            "Custom" => Options.EventTypeCustom,
            _ => Options.EventType.id
        };

        return ary.Where(obj => obj.Type == type);
    }
}