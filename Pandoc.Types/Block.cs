using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public abstract class Block
    {
        private Block() { }

        public sealed class Plain : Block
        {
            public Plain(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Para : Block
        {
            public Para(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class LineBlock : Block
        {
            public LineBlock(IEnumerable<IEnumerable<Inline>> content) : base()
            {
                Content = content.ToNestedImmutableList();
            }
            public ImmutableList<ImmutableList<Inline>> Content { get; }
        }
        public sealed class CodeBlock : Block
        {
            public CodeBlock(Attr attr, string content) : base()
            {
                Attr = attr;
                Content = content ?? "";
            }
            public Attr Attr { get; }
            public string Content { get; }
        }
        public sealed class RawBlock : Block
        {
            public RawBlock(Format format, string content) : base()
            {
                Format = format;
                Content = content;
            }
            public Format Format { get; }
            public string Content { get; }
        }
        public sealed class BlockQuote : Block
        {
            public BlockQuote(IEnumerable<Block> blocks) : base()
            {
                Content = blocks.ToImmutableList();
            }
            public ImmutableList<Block> Content { get; }
        }
        public sealed class OrderedList : Block
        {
            public OrderedList(ListAttributes listAttributes, IEnumerable<IEnumerable<Block>> items) : base()
            {
                ListAttributes = listAttributes;
                Items = items.ToNestedImmutableList();
            }
            public ListAttributes ListAttributes { get; }
            public ImmutableList<ImmutableList<Block>> Items { get; }
        }
        public sealed class BulletList : Block
        {
            public BulletList(IEnumerable<IEnumerable<Block>> items) : base()
            {
                Items = items.ToNestedImmutableList();
            }
            public ImmutableList<ImmutableList<Block>> Items { get; }
        }
        public sealed class DefinitionList : Block
        {
            public DefinitionList(IEnumerable<Definition> definitions) : base()
            {
                Definitions = definitions.ToImmutableList();
            }
            public ImmutableList<Definition> Definitions { get; }
        }
        public sealed class Header : Block
        {
            public Header(int level, Attr attr, IEnumerable<Inline> content) : base()
            {
                Level = level;
                Attr = attr;
                Content = content.ToImmutableList();
            }
            public int Level { get; }
            public Attr Attr { get; }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class HorizontalRule : Block
        {
            public static HorizontalRule Instance { get; } = new HorizontalRule();
            public HorizontalRule() : base() { }
        }
        public sealed class Table : Block
        {
            //Table Attr Caption [ColSpec] TableHead [TableBody] TableFoot
            public Table(Attr attr, Caption caption, IEnumerable<ColSpec> colSpecs,
                TableHead tableHead, IEnumerable<TableBody> contents, TableFoot tableFoot) : base()
            {
                Attr = attr;
                Caption = caption;
                ColSpecs = colSpecs.ToImmutableList();
                TableHead = tableHead;
                Contents = contents.ToImmutableList();
                TableFoot = tableFoot;

            }

            public Attr Attr { get; }
            public Caption Caption { get; }
            public ImmutableList<ColSpec> ColSpecs { get; }
            public TableHead TableHead { get; }
            public ImmutableList<TableBody> Contents { get; }
            public TableFoot TableFoot { get; }
        }
        public sealed class Div : Block
        {
            public Div(Attr attr, IEnumerable<Block> content) : base()
            {
                Attr = attr;
                Content = content.ToImmutableList();
            }
            public Attr Attr { get; }
            public ImmutableList<Block> Content { get; }
        }
        public sealed class Null : Block
        {
            public static Null Instance { get; } = new Null();
            public Null() : base() { }
        }
    }
}