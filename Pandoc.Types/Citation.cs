using System.Collections.Generic;
using System.Collections.Immutable;

namespace Pandoc.Types
{
    public class Citation
    {
        public Citation(string id, IEnumerable<Inline> prefix, IEnumerable<Inline> suffix, CitationMode mode, int noteNum, int hash)
        {
            Id = id ?? "";
            Prefix = prefix.ToImmutableList();
            Suffix = suffix.ToImmutableList();
            Mode = mode;
            NoteNum = noteNum;
            Hash = hash;
        }

        public string Id { get; }
        public ImmutableList<Inline> Prefix { get; }
        public ImmutableList<Inline> Suffix { get; }
        public CitationMode Mode { get; }
        public int NoteNum { get; }
        public int Hash { get; }

        public Citation With(string? id = null, IEnumerable<Inline>? prefix = null, IEnumerable<Inline>? suffix = null, CitationMode? mode = null, int? noteNum = null, int? hash = null)
            => new Citation(id ?? Id, prefix ?? Prefix, suffix ?? Suffix, mode ?? Mode, noteNum ?? NoteNum, hash ?? Hash);
    }
}