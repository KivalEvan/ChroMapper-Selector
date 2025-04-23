using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterEventValue
{
    internal static IEnumerable<BaseEvent> Perform(IEnumerable<BaseEvent> ary)
    {
        if (!Options.EventValue.Enabled) return ary;
        
        {
            ary = Options.EventValueColorDropdown.name switch
            {
                "Off" => ary.Where(obj => obj.IsOff),
                "Blue" => ary.Where(obj => obj.IsBlue),
                "Red" => ary.Where(obj => obj.IsRed),
                "White" => ary.Where(obj => obj.IsWhite),
                "Unknown" => ary.Where(obj => obj.Value < 0 || obj.Value > 12),
                "Custom" => ary.Where(obj => obj.Value == Options.EventValue.Operand1),
                _ => ary
            };
        }

        if (Options.EventValue.Enabled && Options.EventValueColorDropdown.name != "Custom")
        {
            ary = Options.EventValueTypeDropdown.name switch
            {
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