namespace Selector;

internal struct OptionValue<T>()
{
    internal bool Enabled = false;
    internal T Operand1 = default;
    internal T Operand2 = default;
    internal OperationType Operation = OperationType.Equal;
    internal float Tolerance = 0.001f;
}

internal static class Options
{
    public static bool SelectNote = true;
    public static bool SelectBomb = false;
    public static bool SelectEvent = false;
    public static bool SelectObstacle = false;
    public static bool SelectArc = false;
    public static bool SelectChain = false;
    
    public static CustomDataSelectType CustomDataSelect = CustomDataSelectType.Any;

    public static OptionValue<float> Time = new()
    {
        Enabled = true,
        Operand1 = 4.0f,
        Operand2 = 4.0f,
        Operation = OperationType.Additive
    };

    public static OptionValue<int> GridColor = new();
    public static (int id, string name) GridColorDropdown = Items.NoteColors[0];

    public static OptionValue<int> GridDirection = new();
    public static (int id, string name) GridDirectionDropdown = Items.NoteDirections[0];

    public static OptionValue<int> GridX = new();
    public static OptionValue<int> GridY = new();

    public static (int id, string name) EventTypeDropdown = Items.EventTypes[0];
    public static OptionValue<int> EventType = new();

    public static (int id, string name) EventValueColorDropdown = Items.EventValueColors[0];
    public static (int id, string name) EventValueTypeDropdown = Items.EventValueTypes[0];
    public static OptionValue<int> EventValue = new();

    public static OptionValue<float> EventFloatValue = new()
    {
        Operand1 = 1.0f
    };
}