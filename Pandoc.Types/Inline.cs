using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public abstract class Inline
    {
        private Inline() { }
        public sealed class Str : Inline
        {
            public Str(string content) : base()
            {
                Content = content;
            }
            public string Content { get; }
        }
        public sealed class Emph : Inline
        {
            public Emph(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Strong : Inline
        {
            public Strong(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Strikeout : Inline
        {
            public Strikeout(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Superscript : Inline
        {
            public Superscript(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Subscript : Inline
        {
            public Subscript(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class SmallCaps : Inline
        {
            public SmallCaps(IEnumerable<Inline> content) : base()
            {
                Content = content.ToImmutableList();
            }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Quoted : Inline
        {
            public Quoted(QuoteType quoteType, IEnumerable<Inline> content) : base()
            {
                QuoteType = quoteType;
                Content = content.ToImmutableList();
            }

            public QuoteType QuoteType { get; }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Cite : Inline
        {
            public Cite(IEnumerable<Citation> citation, IEnumerable<Inline> content) : base()
            {
                Citation = citation.ToImmutableList();
                Content = content.ToImmutableList();
            }

            public ImmutableList<Citation> Citation { get; }
            public ImmutableList<Inline> Content { get; }
        }
        public sealed class Code : Inline
        {
            public Code(Attr attr, string content) : base()
            {
                Attr = attr;
                Content = content;
            }

            public Attr Attr { get; }
            public string Content { get; }
        }
        public sealed class Space : Inline
        {
            public static Space Instance { get; } = new Space();
            public Space() : base() { }
        }
        public sealed class SoftBreak : Inline
        {
            public static SoftBreak Instance { get; } = new SoftBreak();
            public SoftBreak() : base() { }
        }
        public sealed class LineBreak : Inline
        {
            public static LineBreak Instance { get; } = new LineBreak();
            public LineBreak() : base() { }
        }
        public sealed class Math : Inline
        {
            public Math(MathType mathType, string content) : base()
            {
                MathType = mathType;
                Content = content;
            }

            public MathType MathType { get; }
            public string Content { get; }
        }
        public sealed class RawInline : Inline
        {
            public RawInline(Format format, string content)
            {
                Format = format;
                Content = content;
            }

            public Format Format { get; }
            public string Content { get; }
        }
        public sealed class Link : Inline
        {
            public Link(Attr attr, IEnumerable<Inline> alt, Target target) : base()
            {
                Attr = attr;
                Alt = alt.ToImmutableList();
                Target = target;
            }

            public Attr Attr { get; }
            public ImmutableList<Inline> Alt { get; }
            public Target Target { get; }
        }
        public sealed class Image : Inline
        {
            public Image(Attr attr, IEnumerable<Inline> alt, Target target) : base()
            {
                Attr = attr;
                Alt = alt;
                Target = target;
            }

            public Attr Attr { get; }
            public IEnumerable<Inline> Alt { get; }
            public Target Target { get; }
        }
        public sealed class Note : Inline
        {
            public Note(IEnumerable<Block> content) : base()
            {
                Content = content.ToImmutableList();
            }

            public ImmutableList<Block> Content { get; }
        }
        public sealed class Span : Inline
        {
            public Span(Attr attr, IEnumerable<Inline> content) : base()
            {
                Attr = attr;
                Content = content.ToImmutableList();
            }

            public Attr Attr { get; }
            public ImmutableList<Inline> Content { get; }
        }
    }
}