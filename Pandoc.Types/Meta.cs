using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Meta
    {
        public Meta(ImmutableDictionary<string, MetaValue> map)
        {
            Map = map ?? ImmutableDictionary<string, MetaValue>.Empty;
        }

        public ImmutableDictionary<string, MetaValue> Map { get; }

        public static Meta operator +(Meta left, Meta right)
            => new Meta(left.Map.AddRange(right.Map));

        public static Meta Empty { get; } = new Meta(ImmutableDictionary<string, MetaValue>.Empty);
    }
}