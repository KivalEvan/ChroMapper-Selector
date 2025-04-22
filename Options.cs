namespace Selector;

enum TimeOperation
{
    Until,
    Additive,
    Equal,
    Greater,
    GreaterOrEqual,
    Lesser,
    LesserOrEqual,
}

internal static class Options
{
    public static bool SelectNote { set; get; } = true;
    public static bool SelectBomb { set; get; } = false;
    public static bool SelectEvent { set; get; } = false;
    public static bool SelectObstacle { set; get; } = false;
    public static bool SelectArc { set; get; } = false;
    public static bool SelectChain { set; get; } = false;

    public static float TimeOperand1 { set; get; } = 0.0f;
    public static float TimeOperand2 { set; get; } = 999.0f;
    public static float TimeTolerance { set; get; } = 0.001f;
    public static bool TimeSelect { set; get; } = true;
    public static TimeOperation TimeOperator { set; get; } = TimeOperation.Until;

    public static bool GridColorSelect { set; get; } = false;
    public static bool GridDirectionSelect { set; get; } = false;
    public static bool GridXSelect { set; get; } = false;
    public static bool GridYSelect { set; get; } = false;
    public static (int id, string name) GridColor { set; get; } = Items.NoteColor[0];
    public static (int id, string name) GridDirection { set; get; } = Items.NoteDirection[0];
    public static int GridX { set; get; } = 0;
    public static int GridY { set; get; } = 0;

    public static bool EventTypeSelect { set; get; } = false;
    public static bool EventValueColorSelect { set; get; } = false;
    public static bool EventValueTypeSelect { set; get; } = false;
    public static bool EventFloatValueSelect { set; get; } = false;
    public static (int id, string name) EventType { set; get; } = Items.EventType[0];
    public static int EventTypeCustom { set; get; } = 0;
    public static (int id, string name) EventValueColor { set; get; } = Items.EventValueColor[0];
    public static (int id, string name) EventValueType { set; get; } = Items.EventValueType[0];
    public static int EventValueCustom { set; get; } = 0;
    public static float EventFloatValue { set; get; } = 1.0f;
    public static float EventFloatValueTolerance { set; get; } = 0.001f;
}