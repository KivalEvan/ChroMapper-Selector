using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterEventValue
{
    internal static IEnumerable<BaseEvent> Perform(IEnumerable<BaseEvent> ary)
    {
        if (Options.EventValueColorSelect)
        {
            ary = Options.EventValueColor.name switch
            {
                "Blue" => ary.Where(obj => obj.IsBlue),
                "Red" => ary.Where(obj => obj.IsRed),
                "White" => ary.Where(obj => obj.IsWhite),
                "Unknown" => ary.Where(obj => obj.Value < 0 || obj.Value > 12),
                "Custom" => ary.Where(obj => obj.Value == Options.EventValueCustom),
                _ => ary
            };
        }

        if (Options.EventValueTypeSelect)
        {
            ary = Options.EventValueType.name switch
            {
                "Off" => ary.Where(obj => obj.IsOff),
                "On" => ary.Where(obj => obj.IsOn),
                "Flash" => ary.Where(obj => obj.IsFlash),
                "Fade" => ary.Where(obj => obj.IsFade),
                "Transition" => ary.Where(obj => obj.IsTransition),
                _ => ary
            };
        }

        return ary;
    }
}