using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrgentCast.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrgentCast.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EpisodesController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Episodes
                .OrderByDescending(p => p.PublishedAt)
                .ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
