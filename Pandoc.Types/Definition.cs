using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Definition
    {
        public Definition(IEnumerable<Inline> term, IEnumerable<IEnumerable<Block>> definitions)
        {
            Term = term.ToImmutableList();
            Definitions = definitions.ToNestedImmutableList();
        }
        public ImmutableList<Inline> Term { get; }
        public ImmutableList<ImmutableList<Block>> Definitions { get; }
    }
}