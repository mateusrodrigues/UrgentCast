using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrgentCast.Data;
using UrgentCast.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrgentCast.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EpisodesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storage;

        public EpisodesController (ApplicationDbContext context, IStorageService storage)
        {
            _context = context;
            _storage = storage;
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
            var files = _storage.ListEpisodes();
            ViewBag.MediaUrl = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(files, "Name", "Uri");

            return View();
        }
    }
}
