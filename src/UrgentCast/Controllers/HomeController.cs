using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrgentCast.Data;

namespace UrgentCast.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var feeds = _context.Feeds
                .Include(p => p.Episodes)
                .OrderBy(p => p.Title)
                .ToList();
            
            feeds.ForEach(f => 
            {
                // Order the episodes in descending order of publication date
                f.Episodes = f.Episodes
                    .OrderByDescending(e => e.PublishedAt)
                    .Take(20)
                    .ToList();
            });

            return View(feeds);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
