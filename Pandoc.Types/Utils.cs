using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Pandoc.Types
{
    internal static class Utils
    {
        public static ImmutableList<ImmutableList<T>> ToNestedImmutableList<T>(this IEnumerable<IEnumerable<T>> source)
            => source as ImmutableList<ImmutableList<T>>
            ?? source.Select(x => x.ToImmutableList()).ToImmutableList();
    }
}
