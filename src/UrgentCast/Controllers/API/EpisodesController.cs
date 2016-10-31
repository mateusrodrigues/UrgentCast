using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UrgentCast.Data;

namespace UrgentCast.Controllers.API
{
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController (ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("api/feed/{feedId}/episodes")]
        public IActionResult Get(int feedId)
        {
            var episodes = _context.Episodes
                .Where(p => p.FeedID == feedId)
                .OrderByDescending(p => p.PublishedAt)
                .ToList();
            
            return Ok(episodes);
        }
    }
}