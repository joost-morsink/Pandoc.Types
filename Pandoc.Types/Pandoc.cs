using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Pandoc
    {
        public Pandoc(Meta meta, IEnumerable<Block> blocks)
        {
            Meta = meta;
            Blocks = blocks.ToImmutableList();
        }
        public Meta Meta { get; }
        public ImmutableList<Block> Blocks { get; }
        public Pandoc With(Meta? meta = null, ImmutableList<Block>? blocks = null)
            => new Pandoc(meta ?? Meta, blocks ?? Blocks);

        public static Pandoc operator +(Pandoc left, Pandoc right)
            => new Pandoc(left.Meta + right.Meta, left.Blocks.AddRange(right.Blocks));
    }
}