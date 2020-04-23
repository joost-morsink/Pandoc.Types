using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Row
    {
        public Row(Attr attr, IEnumerable<Cell> cells)
        {
            Attr = attr;
            Cells = cells.ToImmutableList();
        }

        public Attr Attr { get; }
        public ImmutableList<Cell> Cells { get; }
    }
}
