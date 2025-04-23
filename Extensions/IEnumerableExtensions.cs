using System;
using System.Collections.Generic;

namespace Selector.Extensions;

internal static class IEnumerableExtensions
{
    internal static IEnumerable<T> Pipe<T>(this IEnumerable<T> ary, Func<IEnumerable<T>, IEnumerable<T>> func)
    {
        return func(ary);
    }
}