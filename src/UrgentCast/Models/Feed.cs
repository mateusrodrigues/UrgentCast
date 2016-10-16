using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrgentCast.Models
{
    public class Feed
    {
        public int FeedID { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public bool Explicit { get; set; }
        public string ImageURL { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }
    }
}
