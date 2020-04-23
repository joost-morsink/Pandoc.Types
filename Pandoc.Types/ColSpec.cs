using System;
namespace Pandoc.Types
{
    public class ColSpec
    {
        public ColSpec(Alignment alignment, double width)
        {
            Alignment = alignment;
            Width = width;
        }

        public Alignment Alignment { get; }
        public double Width { get; }
    }
}
