using ChristmassCardGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult sendEmail()
        {
            HtmlToPdfConverter converter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();

            settings.WebKitPath = Path.Combine(_env.ContentRootPath, "img");
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
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
