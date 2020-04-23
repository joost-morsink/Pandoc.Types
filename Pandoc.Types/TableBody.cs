using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class TableBody
    {
        public TableBody(Attr attr, int rowHeadColumns, IEnumerable<Row> intermediateHeader, IEnumerable<Row> rows)
        {
            Attr = attr;
            RowHeadColumns = rowHeadColumns;
            IntermediateHeader = intermediateHeader.ToImmutableList();
            Rows = rows.ToImmutableList();
        }

    
        public Attr Attr { get; }
        public int RowHeadColumns { get; }
        public ImmutableList<Row> IntermediateHeader { get; }
        public ImmutableList<Row> Rows { get; }
    }
}
