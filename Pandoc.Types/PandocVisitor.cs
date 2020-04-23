using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Pandoc.Types
{
    public abstract class PandocVisitor<TPandoc, TMeta, TMetaValue, TBlock, TAttr, TInline, TCitation, TCaption, TTableHead, TTableBody, TTableFoot, TRow, TCell>
    {
        public virtual TPandoc PreVisitPandoc(Pandoc p)
            => VisitPandoc(PreVisitMeta(p.Meta), p.Blocks.Select(VisitBlock));
        public abstract TPandoc VisitPandoc(TMeta meta, IEnumerable<TBlock> blocks);
        public virtual TMeta PreVisitMeta(Meta m)
            => VisitMeta(m.Map.Select(kvp => new KeyValuePair<string, TMetaValue>(kvp.Key, VisitMetaValue(kvp.Value))));
        public abstract TMeta VisitMeta(IEnumerable<KeyValuePair<string, TMetaValue>> mappings);

        public virtual TMetaValue VisitMetaValue(MetaValue mv)
            => mv switch
            {
                MetaValue.Map m => PreVisitMetaMap(m),
                MetaValue.List l => PreVisitMetaList(l),
                MetaValue.Bool b => VisitMetaBool(b.Value),
                MetaValue.String s => VisitMetaString(s.Value),
                MetaValue.Inlines il => PreVisitMetaInlines(il),
                MetaValue.Blocks bs => PreVisitMetaBlocks(bs),
                _ => throw new ArgumentException()
            };

        public virtual TMetaValue PreVisitMetaBlocks(MetaValue.Blocks blocks)
            => VisitMetaBlocks(blocks.Values.Select(VisitBlock));
        public abstract TMetaValue VisitMetaBlocks(IEnumerable<TBlock> blocks);
        public virtual TMetaValue PreVisitMetaInlines(MetaValue.Inlines inlines)
            => VisitMetaInlines(inlines.Values.Select(VisitInline));
        public abstract TMetaValue VisitMetaInlines(IEnumerable<TInline> values);

        public abstract TMetaValue VisitMetaString(string value);
        public abstract TMetaValue VisitMetaBool(bool value);
        public virtual TMetaValue PreVisitMetaList(MetaValue.List list)
            => VisitMetaList(list.Values.Select(VisitMetaValue));
        public abstract TMetaValue VisitMetaList(IEnumerable<TMetaValue> values);
        public virtual TMetaValue PreVisitMetaMap(MetaValue.Map map)
            => VisitMetaMap(map.Mappings.Select(kvp => new KeyValuePair<string, TMetaValue>(kvp.Key, VisitMetaValue(kvp.Value))));
        public abstract TMetaValue VisitMetaMap(IEnumerable<KeyValuePair<string, TMetaValue>> mappings);

        public virtual TBlock VisitBlock(Block b)
            => b switch
            {
                Block.Plain pl => PreVisitBlockPlain(pl),
                Block.Para pa => PreVisitBlockPara(pa),
                Block.LineBlock lb => PreVisitBlockLineBlock(lb),
                Block.CodeBlock cb => PreVisitBlockCodeBlock(cb),
                Block.RawBlock rb => VisitBlockRawBlock(rb.Format.Text, rb.Content),
                Block.BlockQuote bq => PreVisitBlockBlockQuote(bq),
                Block.OrderedList ol => PreVisitBlockOrderedList(ol),
                Block.BulletList bl => PreVisitBlockBulletList(bl),
                Block.DefinitionList dl => PreVisitBlockDefinitionList(dl),
                Block.Header hdr => PreVisitBlockHeader(hdr),
                Block.HorizontalRule hr => VisitBlockHorizontalRule(),
                Block.Table tbl => PreVisitBlockTable(tbl),
                Block.Div div => PreVisitBlockDiv(div),
                Block.Null nll => VisitBlockNull(),
                _ => throw new ArgumentException()
            };
        public abstract TBlock VisitBlockNull();

        public virtual TBlock PreVisitBlockDiv(Block.Div div)
            => VisitBlockDiv(VisitAttr(div.Attr), div.Content.Select(VisitBlock));
        public abstract TBlock VisitBlockDiv(TAttr attr, IEnumerable<TBlock> content);

        public virtual TBlock PreVisitBlockTable(Block.Table tbl)
            => VisitBlockTable(VisitAttr(tbl.Attr), PreVisitCaption(tbl.Caption), tbl.ColSpecs, PreVisitTableHead(tbl.TableHead), tbl.Contents.Select(PreVisitTableBody), PreVisitTableFoot(tbl.TableFoot));
        public abstract TBlock VisitBlockTable(TAttr attr, TCaption caption, IEnumerable<ColSpec> colSpecs, TTableHead tableHead, IEnumerable<TTableBody> contents, TTableFoot tableFoot);

        public abstract TBlock VisitBlockHorizontalRule();

        public virtual TBlock PreVisitBlockHeader(Block.Header hdr)
            => VisitBlockHeader(hdr.Level, hdr.Attr, hdr.Content.Select(VisitInline));
        public abstract TBlock VisitBlockHeader(int level, Attr attr, IEnumerable<TInline> content);

        public virtual TBlock PreVisitBlockDefinitionList(Block.DefinitionList dl)
            => VisitBlockDefinitionList(dl.Definitions.AsEnumerable());
        public abstract TBlock VisitBlockDefinitionList(IEnumerable<Definition> definitions);

        public virtual TBlock PreVisitBlockBulletList(Block.BulletList bl)
            => VisitBlockBulletList(bl.Items.Select(x => x.Select(VisitBlock)));
        public abstract TBlock VisitBlockBulletList(IEnumerable<IEnumerable<TBlock>> blocks);

        public virtual TBlock PreVisitBlockOrderedList(Block.OrderedList ol)
            => VisitBlockOrderedList(ol.ListAttributes, ol.Items.Select(x => x.Select(VisitBlock)));
        public abstract TBlock VisitBlockOrderedList(ListAttributes listAttributes, IEnumerable<IEnumerable<TBlock>> items);

        public virtual TBlock PreVisitBlockBlockQuote(Block.BlockQuote bq)
            => VisitBlockBlockQuote(bq.Content.Select(VisitBlock));
        public abstract TBlock VisitBlockBlockQuote(IEnumerable<TBlock> content);

        public abstract TBlock VisitBlockRawBlock(string format, string content);

        public virtual TBlock PreVisitBlockCodeBlock(Block.CodeBlock cb)
            => VisitBlockCodeBlock(VisitAttr(cb.Attr), cb.Content);
        public abstract TBlock VisitBlockCodeBlock(TAttr a, string content);

        public virtual TBlock PreVisitBlockLineBlock(Block.LineBlock lb)
            => VisitBlockLineBlock(lb.Content.Select(x => x.Select(VisitInline)));
        public abstract TBlock VisitBlockLineBlock(IEnumerable<IEnumerable<TInline>> content);

        public virtual TBlock PreVisitBlockPara(Block.Para pa)
            => VisitBlockPara(pa.Content.Select(VisitInline));
        public abstract TBlock VisitBlockPara(IEnumerable<TInline> content);

        public virtual TBlock PreVisitBlockPlain(Block.Plain pl)
            => VisitBlockPlain(pl.Content.Select(VisitInline));
        public abstract TBlock VisitBlockPlain(IEnumerable<TInline> content);

        public virtual TInline VisitInline(Inline inline)
            => inline switch
            {
                Inline.Str s => VisitInlineStr(s.Content),
                Inline.Emph em => PreVisitInlineEmph(em),
                Inline.Strong str => PreVisitInlineStrong(str),
                Inline.Strikeout so => PreVisitInlineStrikeout(so),
                Inline.Superscript sups => PreVisitInlineSuperscript(sups),
                Inline.Subscript subs => PreVisitInlineSubscript(subs),
                Inline.SmallCaps sc => PreVisitInlineSmallCaps(sc),
                Inline.Quoted qu => PreVisitInlineQuoted(qu),
                Inline.Cite ci => PreVisitInlineCite(ci),
                Inline.Code co => PreVisitInlineCode(co),
                Inline.Space sp => VisitInlineSpace(),
                Inline.SoftBreak sb => VisitInlineSoftBreak(),
                Inline.LineBreak lb => VisitInlineLineBreak(),
                Inline.Math ma => VisitInlineMath(ma.MathType, ma.Content),
                Inline.RawInline ri => VisitInlineRawInline(ri.Format.Text, ri.Content),
                Inline.Link li => PreVisitInlineLink(li),
                Inline.Image im => PreVisitInlineImage(im),
                Inline.Note no => PreVisitInlineNote(no),
                Inline.Span sp => PreVisitInlineSpan(sp),
                _ => throw new ArgumentException()
            };

        public virtual TInline PreVisitInlineSpan(Inline.Span sp)
            => VisitInlineSpan(VisitAttr(sp.Attr), sp.Content.Select(VisitInline));
        public abstract TInline VisitInlineSpan(TAttr attr, IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineNote(Inline.Note no)
            => VisitInlineNote(no.Content.Select(VisitBlock));
        public abstract TInline VisitInlineNote(IEnumerable<TBlock> content);

        public virtual TInline PreVisitInlineImage(Inline.Image im)
            => VisitInlineImage(VisitAttr(im.Attr), im.Alt.Select(VisitInline), im.Target);
        public abstract TInline VisitInlineImage(TAttr attr, IEnumerable<TInline> alt, Target target);

        public virtual TInline PreVisitInlineLink(Inline.Link li)
            => VisitInlineLink(VisitAttr(li.Attr), li.Alt.Select(VisitInline), li.Target);
        public abstract TInline VisitInlineLink(TAttr attr, IEnumerable<TInline> alt, Target target);

        public abstract TInline VisitInlineRawInline(string format, string content);

        public abstract TInline VisitInlineMath(MathType mathType, string content);

        public abstract TInline VisitInlineLineBreak();
        public abstract TInline VisitInlineSoftBreak();
        public abstract TInline VisitInlineSpace();

        public virtual TInline PreVisitInlineCode(Inline.Code co)
            => VisitInlineCode(VisitAttr(co.Attr), co.Content);
        public abstract TInline VisitInlineCode(TAttr a, string content);

        public virtual TInline PreVisitInlineCite(Inline.Cite ci)
            => VisitInlineCite(ci.Citation.Select(PreVisitCitation), ci.Content.Select(VisitInline));
        public abstract TInline VisitInlineCite(IEnumerable<TCitation> citation, IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineQuoted(Inline.Quoted qu)
            => VisitInlineQuoted(qu.QuoteType, qu.Content.Select(VisitInline));
        public abstract TInline VisitInlineQuoted(QuoteType quoteType, IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineSmallCaps(Inline.SmallCaps sc)
            => VisitInlineSmallCaps(sc.Content.Select(VisitInline));
        public abstract TInline VisitInlineSmallCaps(IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineSubscript(Inline.Subscript subs)
            => VisitInlineSubscript(subs.Content.Select(VisitInline));
        public abstract TInline VisitInlineSubscript(IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineSuperscript(Inline.Superscript sups)
            => VisitInlineSuperscript(sups.Content.Select(VisitInline));
        public abstract TInline VisitInlineSuperscript(IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineStrikeout(Inline.Strikeout so)
            => VisitInlineStrikeout(so.Content.Select(VisitInline));
        public abstract TInline VisitInlineStrikeout(IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineStrong(Inline.Strong str)
            => VisitInlineStrong(str.Content.Select(VisitInline));
        public abstract TInline VisitInlineStrong(IEnumerable<TInline> content);

        public virtual TInline PreVisitInlineEmph(Inline.Emph em)
            => VisitInlineEmph(em.Content.Select(VisitInline));
        public abstract TInline VisitInlineEmph(IEnumerable<TInline> content);

        public abstract TInline VisitInlineStr(string content);

        public abstract TAttr VisitAttr(Attr a);

        public virtual TCitation PreVisitCitation(Citation citation)
            => VisitCitation(citation.Id, citation.Prefix.Select(VisitInline), citation.Suffix.Select(VisitInline), citation.Mode, citation.NoteNum, citation.Hash);
        public abstract TCitation VisitCitation(string id, IEnumerable<TInline> prefix, IEnumerable<TInline> suffix, CitationMode mode, int noteNum, int hash);

        public virtual TCaption PreVisitCaption(Caption caption)
            => VisitCaption(caption.ShortCaption, caption.Content);
        public abstract TCaption VisitCaption(IEnumerable<Inline> shortCaption, IEnumerable<Block> content);
        public virtual TTableHead PreVisitTableHead(TableHead tableHead)
            => VisitTableHead(VisitAttr(tableHead.Attr), tableHead.Rows.Select(PreVisitHeaderRow));
        public abstract TTableHead VisitTableHead(TAttr a, IEnumerable<TRow> enumerable);
        public virtual TTableFoot PreVisitTableFoot(TableFoot tableFoot)
            => VisitTableFoot(VisitAttr(tableFoot.Attr), tableFoot.Rows.Select(PreVisitFooterRow));
        public abstract TTableFoot VisitTableFoot(TAttr a, IEnumerable<TRow> enumerable);
        public virtual TRow PreVisitHeaderRow(Row row)
            => PreVisitRow(row, VisitHeaderRow);
        public virtual TRow PreVisitFooterRow(Row row)
            => PreVisitRow(row, VisitFooterRow);
        public virtual TRow VisitHeaderRow(TAttr attr, IEnumerable<TCell> cells)
            => VisitRow(attr, cells);
        public virtual TRow VisitFooterRow(TAttr attr, IEnumerable<TCell> cells)
            => VisitRow(attr, cells);
        public virtual TRow VisitBodyRow(TAttr attr, IEnumerable<TCell> cells)
            => VisitRow(attr, cells);
        public virtual TRow PreVisitRow(Row row, Func<TAttr, IEnumerable<TCell>, TRow> variant = null!)
            => (variant ?? VisitRow)(VisitAttr(row.Attr), row.Cells.Select(PreVisitHeaderCell));
        public abstract TRow VisitRow(TAttr attr, IEnumerable<TCell> cells);
        public virtual TCell PreVisitHeaderCell(Cell cell)
            => PreVisitCell(cell);
        public virtual TCell PreVisitBodyCell(Cell cell)
            => PreVisitCell(cell);
        public virtual TCell PreVisitFooterCell(Cell cell)
            => PreVisitCell(cell);
        public virtual TCell PreVisitCell(Cell cell)
            => VisitCell(VisitAttr(cell.Attr), cell.Alignment, cell.RowSpan, cell.ColSpan, cell.Content.Select(VisitBlock));
        public abstract TCell VisitCell(TAttr attr, Alignment alignment, int rowSpan, int colSpan, IEnumerable<TBlock> blocks);

        public virtual TTableBody PreVisitTableBody(TableBody tableBody)
            => VisitTableBody(VisitAttr(tableBody.Attr), tableBody.RowHeadColumns, tableBody.IntermediateHeader.Select(PreVisitHeaderRow), tableBody.Rows.Select(r => PreVisitRow(r, VisitBodyRow)));
        public abstract TTableBody VisitTableBody(TAttr a, int rowHeadColumns, IEnumerable<TRow> enumerable, IEnumerable<TRow> rows);
    }
    public class PandocVisitor : PandocVisitor<Pandoc, Meta, MetaValue, Block, Attr, Inline, Citation, Caption, TableHead, TableBody, TableFoot, Row, Cell>
    {
        public override Attr VisitAttr(Attr a) => a;
        public override Block VisitBlockBlockQuote(IEnumerable<Block> content) => new Block.BlockQuote(content);
        public override Block VisitBlockBulletList(IEnumerable<IEnumerable<Block>> blocks) => new Block.BulletList(blocks);
        public override Block VisitBlockCodeBlock(Attr a, string content) => new Block.CodeBlock(a, content);
        public override Block VisitBlockDefinitionList(IEnumerable<Definition> definitions) => new Block.DefinitionList(definitions);
        public override Block VisitBlockDiv(Attr attr, IEnumerable<Block> content) => new Block.Div(attr, content);
        public override Block VisitBlockHeader(int level, Attr attr, IEnumerable<Inline> content) => new Block.Header(level, attr, content);
        public override Block VisitBlockHorizontalRule() => Block.HorizontalRule.Instance;
        public override Block VisitBlockLineBlock(IEnumerable<IEnumerable<Inline>> content) => new Block.LineBlock(content);
        public override Block VisitBlockNull() => Block.Null.Instance;
        public override Block VisitBlockOrderedList(ListAttributes listAttributes, IEnumerable<IEnumerable<Block>> items) => new Block.OrderedList(listAttributes, items);
        public override Block VisitBlockPara(IEnumerable<Inline> content) => new Block.Para(content);
        public override Block VisitBlockPlain(IEnumerable<Inline> content) => new Block.Plain(content);
        public override Block VisitBlockRawBlock(string format, string content) => new Block.RawBlock(new Format(format), content);
        public override Block VisitBlockTable(Attr attr, Caption caption, IEnumerable<ColSpec> colSpecs, TableHead tableHead, IEnumerable<TableBody> contents, TableFoot tableFoot)
            => new Block.Table(attr, caption, colSpecs, tableHead, contents, tableFoot);
        public override Caption VisitCaption(IEnumerable<Inline> shortCaption, IEnumerable<Block> content) => new Caption(shortCaption, content);
        public override Cell VisitCell(Attr attr, Alignment alignment, int rowSpan, int colSpan, IEnumerable<Block> blocks)
            => new Cell(attr, alignment, rowSpan, colSpan, blocks);
        public override Citation VisitCitation(string id, IEnumerable<Inline> prefix, IEnumerable<Inline> suffix, CitationMode mode, int noteNum, int hash)
            => new Citation(id, prefix, suffix, mode, noteNum, hash);
        public override Inline VisitInlineCite(IEnumerable<Citation> citation, IEnumerable<Inline> content) => new Inline.Cite(citation, content);
        public override Inline VisitInlineCode(Attr a, string content) => new Inline.Code(a, content);
        public override Inline VisitInlineEmph(IEnumerable<Inline> content) => new Inline.Emph(content);
        public override Inline VisitInlineImage(Attr attr, IEnumerable<Inline> alt, Target target) => new Inline.Image(attr, alt, target);
        public override Inline VisitInlineLineBreak() => Inline.LineBreak.Instance;
        public override Inline VisitInlineLink(Attr attr, IEnumerable<Inline> alt, Target target) => new Inline.Link(attr, alt, target);
        public override Inline VisitInlineMath(MathType mathType, string content) => new Inline.Math(mathType, content);
        public override Inline VisitInlineNote(IEnumerable<Block> content) => new Inline.Note(content);
        public override Inline VisitInlineQuoted(QuoteType quoteType, IEnumerable<Inline> content) => new Inline.Quoted(quoteType, content);
        public override Inline VisitInlineRawInline(string format, string content) => new Inline.RawInline(new Format(format), content);
        public override Inline VisitInlineSmallCaps(IEnumerable<Inline> content) => new Inline.SmallCaps(content);
        public override Inline VisitInlineSoftBreak() => Inline.SoftBreak.Instance;
        public override Inline VisitInlineSpace() => Inline.Space.Instance;
        public override Inline VisitInlineSpan(Attr attr, IEnumerable<Inline> content) => new Inline.Span(attr, content);
        public override Inline VisitInlineStr(string content) => new Inline.Str(content);
        public override Inline VisitInlineStrikeout(IEnumerable<Inline> content) => new Inline.Strikeout(content);
        public override Inline VisitInlineStrong(IEnumerable<Inline> content) => new Inline.Strong(content);
        public override Inline VisitInlineSubscript(IEnumerable<Inline> content) => new Inline.Subscript(content);
        public override Inline VisitInlineSuperscript(IEnumerable<Inline> content) => new Inline.Superscript(content);
        public override Meta VisitMeta(IEnumerable<KeyValuePair<string, MetaValue>> mappings) => new Meta(mappings.ToImmutableDictionary());
        public override MetaValue VisitMetaBlocks(IEnumerable<Block> blocks) => new MetaValue.Blocks(blocks);
        public override MetaValue VisitMetaBool(bool value) => value;
        public override MetaValue VisitMetaInlines(IEnumerable<Inline> values) => new MetaValue.Inlines(values);
        public override MetaValue VisitMetaList(IEnumerable<MetaValue> values) => new MetaValue.List(values);
        public override MetaValue VisitMetaMap(IEnumerable<KeyValuePair<string, MetaValue>> mappings) => new MetaValue.Map(mappings.ToImmutableDictionary());
        public override MetaValue VisitMetaString(string value) => value;
        public override Pandoc VisitPandoc(Meta meta, IEnumerable<Block> blocks) => new Pandoc(meta, blocks);
        public override Row VisitRow(Attr attr, IEnumerable<Cell> cells) => new Row(attr, cells);
        public override TableBody VisitTableBody(Attr attr, int rowHeadColumns, IEnumerable<Row> intermediateHeader, IEnumerable<Row> rows)
            => new TableBody(attr, rowHeadColumns, intermediateHeader, rows);
        public override TableFoot VisitTableFoot(Attr attr, IEnumerable<Row> rows) => new TableFoot(attr, rows);
        public override TableHead VisitTableHead(Attr attr, IEnumerable<Row> rows) => new TableHead(attr, rows);
    }
}