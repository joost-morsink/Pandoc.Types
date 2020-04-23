﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class TableFoot
    {
        public TableFoot(Attr attr, IEnumerable<Row> rows)
        {
            Attr = attr;
            Rows = rows.ToImmutableList();
        }

        public Attr Attr { get; }
        public ImmutableList<Row> Rows { get; }
    }
}
