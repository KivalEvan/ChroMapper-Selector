using System;
using System.Collections.Generic;
using System.Linq;
using Beatmap.Base;

namespace Selector.Actions;

internal static class FilterTime
{
    private static float _adjustTimeOp1;
    private static float _adjustTimeOp2;

    internal static IEnumerable<T> Perform<T>(IEnumerable<T> ary) where T : BaseObject
    {
        if (!Options.Time.Enabled) return ary;
        _adjustTimeOp1 = Options.Time.Operand1;
        _adjustTimeOp2 = Options.Time.Operand2;

        Func<BaseObject, bool> action = Options.Time.Operation switch
        {
            OperationType.Range => OpRange,
            OperationType.Additive => OpAdditive,
            OperationType.Equal => OpEqual,
            OperationType.NotEqual => OpNotEqual,
            OperationType.Greater => OpGreater,
            OperationType.GreaterOrEqual => OpGreaterOrEqual,
            OperationType.Lesser => OpLesser,
            OperationType.LesserOrEqual => OpLesserOrEqual,
            _ => OpRange
        };

        return ary.Where<T>(action);
    }
    
    private static bool OpRange<T>(T obj) where T : BaseObject
    {
        return obj.JsonTime >= _adjustTimeOp1 - Options.Time.Tolerance && obj.JsonTime <= _adjustTimeOp2 + Options.Time.Tolerance;
    }
    
    private static bool OpAdditive<T>(T obj) where T : BaseObject
    {
        return obj.JsonTime >= _adjustTimeOp1 - Options.Time.Tolerance && obj.JsonTime <= _adjustTimeOp1 + _adjustTimeOp2 + Options.Time.Tolerance;
    }
    
    private static bool OpEqual<T>(T obj) where T : BaseObject
    {
        return Math.Abs(obj.JsonTime - _adjustTimeOp1) <= Options.Time.Tolerance;
    }
    
    private static bool OpNotEqual<T>(T obj) where T : BaseObject
    {
        return !OpEqual(obj);
    }

    private static bool OpGreater<T>(T obj) where T : BaseObject
    {
        return _adjustTimeOp1 > obj.JsonTime;
    }
    
    private static bool OpGreaterOrEqual<T>(T obj) where T : BaseObject
    {
        return _adjustTimeOp1 >= obj.JsonTime;
    }
    
    private static bool OpLesser<T>(T obj) where T : BaseObject
    {
        return _adjustTimeOp1 < obj.JsonTime;
    }
    
    private static bool OpLesserOrEqual<T>(T obj) where T : BaseObject
    {
        return _adjustTimeOp1 <= obj.JsonTime;
    }
}