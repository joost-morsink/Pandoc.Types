namespace Pandoc.Types
{
    public class ListAttributes
    {
        public ListAttributes(int startNumber, ListNumberStyle style, ListNumberDelim delimiter)
        {
            StartNumber = startNumber;
            Style = style;
            Delimiter = delimiter;
        }

        public int StartNumber { get; }
        public ListNumberStyle Style { get; }
        public ListNumberDelim Delimiter { get; }
        public ListAttributes With(int? startNumber = null, ListNumberStyle? style = null, ListNumberDelim? delimiter = null)
            => new ListAttributes(startNumber ?? StartNumber, style ?? Style, delimiter ?? Delimiter);
    }
}