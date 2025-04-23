using System.Collections.Generic;
using Beatmap.Enums;

namespace Selector;

enum ObjectSelectType
{
    Note,
    Bomb,
    Arc,
    Chain,
    Obstacle,
    Event
}

enum OperationType
{
    Range,
    Additive,
    NotEqual,
    Equal,
    Greater,
    GreaterOrEqual,
    Lesser,
    LesserOrEqual
}

internal static class Items
{
    internal static readonly List<(OperationType op, string name, bool hasOp2)> Operators =
    [
        (OperationType.Additive, "Additive", true),
        (OperationType.Range, "Range", true),
        (OperationType.Equal, "==", false),
        (OperationType.NotEqual, "!=", false),
        (OperationType.Greater, ">", false),
        (OperationType.GreaterOrEqual, ">=", false),
        (OperationType.Lesser, "<", false),
        (OperationType.LesserOrEqual, "<=", false)
    ];
    
    internal static readonly List<(int id, string name)> EventTypes =
    [
        (0, "Back top"),
        (1, "Ring"),
        (2, "L Laser"),
        (3, "R Laser"),
        (4, "Center"),
        (5, "Boost"),
        (6, "XL Light"),
        (7, "XR Light"),
        (8, "Ring Rot"),
        (9, "Ring Zoom"),
        (10, "XL Laser"),
        (11, "XR Laser"),
        (12, "L Rot"),
        (13, "R Rot"),
        (14, "Early Rot"),
        (15, "Late Rot"),
        (16, "Utility 0"),
        (17, "Utility 1"),
        (18, "Utility 2"),
        (19, "Utility 3"),
        (40, "Special 0"),
        (41, "Special 1"),
        (42, "Special 2"),
        (43, "Special 3"),
        (100, "BPM"),
        (-1, "Custom")
    ];

    internal static readonly List<(int id, string name)> EventValueColors =
    [
        (0, "Off"),
        (1, "Blue"),
        (5, "Red"),
        (9, "White"),
        (-1, "Unknown"),
        (-1, "Custom")
    ];

    internal static readonly List<(int id, string name)> EventValueTypes =
    [
        (0, "On"),
        (1, "Flash"),
        (2, "Fade"),
        (3, "Transition")
    ];

    internal static readonly List<(int id, string name)> NoteColors =
    [
        ((int)NoteColor.Blue, "Blue"),
        ((int)NoteColor.Red, "Red")
    ];

    internal static readonly List<(int id, string name)> NoteDirections =
    [
        (0, "Up"),
        (1, "Down"),
        (2, "Left"),
        (3, "Right"),
        (4, "Up-Left"),
        (5, "Up-Right"),
        (6, "Down-Left"),
        (7, "Down-Right"),
        (8, "Any"),
        (-1, "Unknown"),
        (-1, "ME")
    ];
}