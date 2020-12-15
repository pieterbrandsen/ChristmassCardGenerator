using ChristmassCardGenerator.DAL;
using ChristmassCardGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChristmassCardGenerator.Constants;

namespace ChristmassCardGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        public async Task<IActionResult> Card(string saveButton, string loadButton, [Bind("ID,CardTitle,FromTitle,ImageName,Message")] Card card)
        {
            if (saveButton != null) return RedirectToActionPermanent("SaveCard", card);
            else if (loadButton != null) return RedirectToActionPermanent("LoadCard");
            else return RedirectToActionPermanent("Index");
        }

        public async Task<IActionResult> SaveCard([Bind("ID,CardTitle,FromTitle,ImageName,Message")] Card card)
        {
            if (card.ApplicationUser == null) card.ApplicationUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            if (card.CardTitle == null) card.CardTitle = DefaultCardValues.CardTitle;
            if (card.FromTitle == null) card.FromTitle = DefaultCardValues.FromTitle;
            if (card.ImageName == null) card.ImageName = DefaultCardValues.ImageName;
            if (card.Message == null) card.Message = DefaultCardValues.Message;

            _context.Add(card);
            await _context.SaveChangesAsync();

            return View();
        }

        public IActionResult LoadCard()
        {
            return View(_context.Cards.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser.UserName == User.Identity.Name));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
