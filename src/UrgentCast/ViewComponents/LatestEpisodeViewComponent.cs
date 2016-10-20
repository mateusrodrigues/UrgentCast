using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrgentCast.Data;
using UrgentCast.Models;

namespace UrgentCast.ViewComponents
{
    public class LatestEpisodeViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LatestEpisodeViewComponent (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int maxPriority, bool isDone)
        {
            var episode = _context.Episodes
                .OrderByDescending(e => e.PublishedAt)
                .FirstOrDefault();

            return View<Episode>(episode);
        }
    }
}