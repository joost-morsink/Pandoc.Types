namespace Pandoc.Types
{
    public class Target
    {
        public Target(string url, string title)
        {
            Url = url ?? "";
            Title = title ?? "";
        }

        public string Url { get; }
        public string Title { get; }
        public Target With(string? url = null, string? title = null)
            => new Target(url ?? Url, title ?? Title);
    }
}