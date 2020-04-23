using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public abstract class MetaValue
    {
        private MetaValue() { }
        public sealed class Map : MetaValue
        {
            public Map(ImmutableDictionary<string, MetaValue> mappings) : base()
            {
                Mappings = mappings;
            }
            public ImmutableDictionary<string, MetaValue> Mappings { get; }
        }
        public sealed class List : MetaValue
        {
            public List(IEnumerable<MetaValue> values) : base()
            {
                Values = values.ToImmutableList();
            }
            public ImmutableList<MetaValue> Values { get; }
        }
        public sealed class Bool : MetaValue
        {
            public Bool(bool value) : base()
            {
                Value = value;
            }
            public bool Value { get; }
        }
        public sealed class String : MetaValue
        {
            public String(string value) : base()
            {
                Value = value;
            }
            public string Value { get; }
        }
        public sealed class Inlines : MetaValue
        {
            public Inlines(IEnumerable<Inline> values)
            {
                Values = values.ToImmutableList();
            }
            public ImmutableList<Inline> Values { get; }
        }
        public sealed class Blocks : MetaValue
        {
            public Blocks(IEnumerable<Block> values)
            {
                Values = values.ToImmutableList();
            }
            public ImmutableList<Block> Values { get; }
        }

        public static implicit operator MetaValue(ImmutableDictionary<string, MetaValue> map)
            => new Map(map);
        public static implicit operator MetaValue(Dictionary<string, MetaValue> map)
            => new Map(map.ToImmutableDictionary());

        public static implicit operator MetaValue(MetaValue[] metaValues)
            => new List(metaValues);
        public static implicit operator MetaValue(ImmutableList<MetaValue> metaValues)
            => new List(metaValues);

        public static implicit operator MetaValue(bool value)
            => new Bool(value);

        public static implicit operator MetaValue(string value)
            => new String(value);

        public static implicit operator MetaValue(Inline[] inlines)
            => new Inlines(inlines);
        public static implicit operator MetaValue(ImmutableList<Inline> inlines)
            => new Inlines(inlines);

        public static implicit operator MetaValue(Block[] blocks)
            => new Blocks(blocks);
        public static implicit operator MetaValue(ImmutableList<Block> blocks)
            => new Blocks(blocks);
    }
}