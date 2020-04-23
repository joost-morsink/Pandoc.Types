using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Cell
    {
        public Cell(Attr attr, Alignment alignment, int rowSpan, int colSpan, IEnumerable<Block> content)
        {
            Attr = attr;
            Alignment = alignment;
            RowSpan = rowSpan;
            ColSpan = colSpan;
            Content = content.ToImmutableList();
        }

        public Attr Attr { get; }
        public Alignment Alignment { get; }
        public int RowSpan { get; }
        public int ColSpan { get; }
        public ImmutableList<Block> Content { get; }
    }
}