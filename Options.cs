namespace Selector
{
    static class Options
    {
        public static bool SelectNote { set; get; } = true;
        public static bool SelectEvent { set; get; } = false;
        public static bool SelectObstacle { set; get; } = false;
        public static float TimeStart { set; get; } = 0.0f;
        public static float TimeEnd { set; get; } = 999.0f;
        public static bool StrictType { set; get; } = false;
        public static int Type { set; get; } = 0;
        public static bool StrictValue { set; get; } = false;
        public static int Value { set; get; } = 0;
    }
}