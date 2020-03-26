using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryRedux.Models;

namespace LibraryRedux.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List<Book> Library = new List<Book>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UpdateLibrary(Library);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<Book> UpdateLibrary(List<Book> library)
        {
            LibraryDBContext db = new LibraryDBContext();
            library = db.Book.ToList<Book>();
            return library;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
