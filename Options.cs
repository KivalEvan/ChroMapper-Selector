using System.Collections.Generic;

namespace Selector
{
    internal static class Items
    {
        public static readonly List<string> EventType = new List<string> {
            "0 - Back top",
            "1 - Ring",
            "2 - L Laser",
            "3 - R Laser",
            "4 - Center",
            "5 - Boost",
            "6 - XL Light",
            "7 - XR Light",
            "8 - Ring Rot",
            "9 - Ring Zoom",
            "10 - XL Laser",
            "11 - XR Laser",
            "12 - L Rot",
            "13 - R Rot",
            "14 - Early Rot",
            "15 - Late Rot",
            "16 - Utility 0",
            "17 - Utility 1",
            "18 - Utility 2",
            "19 - Utility 3",
            "40 - Special 0",
            "41 - Special 1",
            "42 - Special 2",
            "43 - Special 3",
            "100 - BPM",
            "Custom"
        };
        public static readonly List<string> EventValueColor = new List<string> { "Blue", "Red", "White", "Unknown", "Custom" };
        public static readonly List<string> EventValueType = new List<string> { "Off", "On", "Flash", "Fade", "Transition" };
        public static readonly List<string> NoteColor = new List<string> { "Blue", "Red" };
        public static readonly List<string> NoteDirection = new List<string>
        {
            "Up",
            "Down",
            "Left",
            "Right",
            "Up-Left",
            "Up-Right",
            "Down-Left",
            "Down-Right",
            "Any",
            "Unknown",
            "ME"
        };
    }
    
    internal static class Options
    {
        public static bool SelectNote { set; get; } = true;
        public static bool SelectBomb { set; get; } = false;
        public static bool SelectEvent { set; get; } = false;
        public static bool SelectObstacle { set; get; } = false;
        public static bool SelectArc { set; get; } = false;
        public static bool SelectChain { set; get; } = false;

        public static float TimeStart { set; get; } = 0.0f;
        public static float TimeEnd { set; get; } = 999.0f;
        public static float TimeTolerance { set; get; } = 0.001f;
        public static bool TimeSelect { set; get; } = true;

        public static bool GridColorSelect { set; get; } = false;
        public static bool GridDirectionSelect { set; get; } = false;
        public static bool GridXSelect { set; get; } = false;
        public static bool GridYSelect { set; get; } = false;
        public static string GridColor { set; get; } = Items.NoteColor[0];
        public static string GridDirection { set; get; } = Items.NoteDirection[0];
        public static int GridX { set; get; } = 0;
        public static int GridY { set; get; } = 0;

        public static bool EventTypeSelect { set; get; } = false;
        public static bool EventValueColorSelect { set; get; } = false;
        public static bool EventValueTypeSelect { set; get; } = false;
        public static bool EventFloatValueSelect { set; get; } = false;
        public static string EventType { set; get; } = Items.EventType[0];
        public static int EventTypeCustom { set; get; } = 0;
        public static string EventValueColor { set; get; } = Items.EventValueColor[0];
        public static string EventValueType { set; get; } = Items.EventValueType[0];
        public static int EventValueCustom { set; get; } = 0;
        public static float EventFloatValue { set; get; } = 1.0f;
        public static float EventFloatValueTolerance { set; get; } = 0.001f;
    }
}