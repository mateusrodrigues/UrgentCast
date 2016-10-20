using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrgentCast.Models
{
    public class Episode
    {
        public int EpisodeID { get; set; }
        public int ShowNumber { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string MediaUrl { get; set; }
        public string Author { get; set; }
        public bool Explicit { get; set; }
        public DateTime PublishedAt { get; set; }
        public int FeedID { get; set; }

        public virtual Feed Feed { get; set; }
    }
}
