using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Caption
    {
        public Caption(IEnumerable<Inline> shortCaption, IEnumerable<Block> content)
        {
            ShortCaption = shortCaption.ToImmutableList();
            Content = content.ToImmutableList();
        }
        public ImmutableList<Inline> ShortCaption { get; set; } // Maybe in Haskell version.
        public ImmutableList<Block> Content { get; set; }
    }
}
