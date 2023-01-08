namespace Selector
{
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

        public static bool GridColorSelect { set; get; } = false;
        public static bool GridDirectionSelect { set; get; } = false;
        public static bool GridXSelect { set; get; } = false;
        public static bool GridYSelect { set; get; } = false;
        public static int GridColor { set; get; } = 0;
        public static int GridDirection { set; get; } = 0;
        public static int GridX { set; get; } = 0;
        public static int GridY { set; get; } = 0;

        public static bool EventTypeSelect { set; get; } = false;
        public static bool EventValueSelect { set; get; } = false;
        public static bool EventFloatValueSelect { set; get; } = false;
        public static int EventType { set; get; } = 0;
        public static int EventValue { set; get; } = 0;
        public static float EventFloatValue { set; get; } = 1.0f;
        public static float EventFloatValueTolerance { set; get; } = 0.001f;
    }
}