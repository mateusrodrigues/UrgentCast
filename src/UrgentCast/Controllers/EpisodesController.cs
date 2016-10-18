using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrgentCast.Data;
using UrgentCast.Models;
using UrgentCast.Models.EpisodesViewModels;
using UrgentCast.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrgentCast.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EpisodesController : Controller
    {
        private readonly int LOCAL_TIMEZONE = -3;

        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storage;

        public EpisodesController (ApplicationDbContext context, IStorageService storage)
        {
            _context = context;
            _storage = storage;
        }

        public IActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
            }

            var model = _context.Episodes
                .Include(p => p.Feed)
                .OrderByDescending(p => p.PublishedAt)
                .ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            var files = _storage.ListEpisodes();
            var feeds = _context.Feeds.ToList();

            ViewBag.MediaUrl = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(files, "Uri", "Name");
            ViewBag.FeedID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(feeds, "FeedID", "Title");

            return View();
        }

        [HttpPost]
        public IActionResult Create(AddEpisodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episode = new Episode
                {
                    Title = model.Title,
                    Subtitle = model.Subtitle,
                    Description = model.Description,
                    MediaUrl = model.MediaUrl,
                    Author = model.Author,
                    Explicit = model.Explicit,
                    PublishedAt = DateTime.UtcNow.AddHours(LOCAL_TIMEZONE),
                    FeedID = model.FeedID
                };

                _context.Episodes.Add(episode);
                _context.SaveChanges();

                return RedirectToAction(nameof(ManageController.Index), new { message = "Episode created" });
            }

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var episode = _context.Episodes.FirstOrDefault(e => e.EpisodeID == id);

            return View(episode);
        }

        [HttpPost]
        public IActionResult Delete(Episode episode)
        {
            _context.Episodes.Remove(episode);
            _context.SaveChanges();

            return RedirectToAction(nameof(EpisodesController.Index));
        }
    }
}
