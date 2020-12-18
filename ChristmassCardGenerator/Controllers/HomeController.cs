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
using static ChristmassCardGenerator.ViewModels.Controllers.HomeControllerViewModel;
using System.Text.Json;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ChristmassCardGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult sendEmail()
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();

            converter.ConverterSettings = settings;

            PdfDocument document = converter.Convert("wwwroot\\img\\Pony.png");

            MemoryStream ms = new MemoryStream();
            document.Save(ms);
            document.Close(true);

            ms.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(ms, "application/pdf");
            fileStreamResult.FileDownloadName = "test.pdf";

            return fileStreamResult;
        }
        public async Task<IActionResult> Index(int? id)
            settings.WebKitPath = Path.Combine(_env.ContentRootPath, "img");
        {
            if (id == null)
            {
                Card newCard = new Card
                {
                    CardTitle = DefaultCardValues.CardTitle,
                    FromTitle = DefaultCardValues.FromTitle,
                    ImageName = DefaultCardValues.ImageName,
                    Message = DefaultCardValues.Message
                };
                return View(newCard);
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Card(string saveButton, string loadButton, string sendButton, [Bind("ID,CardTitle,FromTitle,ImageName,Message")] Card card)
        {
            if (saveButton != null) return RedirectToActionPermanent("SaveCard", card);
            else if (loadButton != null) return RedirectToActionPermanent("LoadCard");
            else if (sendButton != null) return RedirectToActionPermanent("SendCard", card);
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

            return RedirectToActionPermanent("Success");
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

        public async Task<IActionResult> SendCard([Bind("ID,CardTitle,FromTitle,ImageName,Message")] Card card)
        {
            var list = await _context.EmailLists.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser.UserName == User.Identity.Name).ToListAsync();
            var groupedList = list.GroupBy(c => c.ContactType).ToList();

            SendCardViewModel viewModel = new SendCardViewModel();
            viewModel.Card = card;
            viewModel.List = groupedList;
            TempData["Card"] = JsonSerializer.Serialize(card);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendCard(string id)
        {
            string contactType = id;
            Card card = JsonSerializer.Deserialize<Card>(TempData["Card"] as string);
            if (card == null)
            {
                return NotFound();
            }
            else TempData["Card"] = null;

            var list = await _context.EmailLists.Include(e => e.ApplicationUser).Where(e => (contactType == "All" || e.ContactType == (ContactTypes)Enum.Parse(typeof(ContactTypes), contactType)) && e.ApplicationUser.UserName == User.Identity.Name).ToListAsync();
            if (list == null)
            {
                return NotFound();
            }
            var emailAddresses = list.Select(l => l.Email);

            return RedirectToActionPermanent("Success");
        }
    }
}
