using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Attr
    {
        public Attr(string identifier, IEnumerable<string> classes, IEnumerable<KeyValuePair<string, string>> mappings)
        {
            Identifier = identifier ?? "";
            Classes = classes?.ToImmutableList() ?? ImmutableList<string>.Empty;
            Mappings = mappings.ToImmutableDictionary() ?? ImmutableDictionary<string, string>.Empty;
        }

        public string Identifier { get; }
        public ImmutableList<string> Classes { get; }
        public ImmutableDictionary<string, string> Mappings { get; }

        public Attr With(string? identifier = null, IEnumerable<string>? classes = null, IEnumerable<KeyValuePair<string, string>>? mappings = null)
            => new Attr(identifier ?? Identifier, classes ?? Classes, mappings ?? Mappings);

        public static Attr Default { get; } = new Attr("", ImmutableList<string>.Empty, ImmutableDictionary<string, string>.Empty);
    }
}