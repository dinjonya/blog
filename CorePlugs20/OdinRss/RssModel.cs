using System.Collections.Generic;

namespace CorePlugs20.OdinRss
{
    public class AtomModel
    {
        public string RssTitle { get; set; }
        public string AlternateLink { get; set; }
        public string SelfLink { get; set; }
        public AtomTag Tag { get; set; }
        public string SubTitle { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }

        public List<RssModel> Entries { get; set; }

    }

    public class AtomTag
    {
        public string Id { get; set; }
        public string Domain { get; set; }
    }

    public class RssModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Published { get; set; }
        public string Updated { get; set; }
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Contents { get; set; }

    }
}