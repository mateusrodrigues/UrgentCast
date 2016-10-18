using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrgentCast.Data;
using UrgentCast.Models;
using UrgentCast.Models.FeedsViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrgentCast.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FeedsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeedsController (ApplicationDbContext context)
        {
            _context = context;    
        }

        public IActionResult Index()
        {
            var feeds = _context.Feeds
                .Include(p => p.Episodes)
                .ToList();

            return View(feeds);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddFeedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var feed = new Feed
                {
                    Title = model.Title,
                    Link = model.Link,
                    Description = model.Description,
                    Language = model.Language,
                    Author = model.Author,
                    Email = model.Email,
                    Explicit = model.Explicit,
                    ImageURL = model.ImageURL,
                    Category = model.Category
                };

                _context.Feeds.Add(feed);
                _context.SaveChanges();

                return RedirectToAction(nameof(ManageController.Index));
            }

            return View(model);
        }
    }
}
