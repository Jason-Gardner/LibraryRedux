using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryRedux.Models;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Index()
        {
            UpdateLibrary(Library);
            return View();
        }

        [Authorize]
        public IActionResult Menu(string menu)
        {
            if (menu == "search")
            {
                return View("Search");
            }
            else
            {
                return View("Return");
            }
        }

        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult SearchLibrary(string search)
        {
            List<Book> searchList = new List<Book>();
            LibraryDBContext db = new LibraryDBContext();
            foreach (Book book in db.Book)
            {
                if (book.Title.ToLower().Contains(search) | book.Author.ToLower().Contains(search.Trim().ToLower()))
                {
                    searchList.Add(book);
                }
            }

            return View("Search", searchList);
        }

        public IActionResult CheckOut(string book)
        {
            LibraryDBContext db = new LibraryDBContext();
            Book tempBook = new Book();
            foreach (Book entry in db.Book)
            {
                if (entry.Title == book)
                {
                    tempBook = entry;
                    entry.Checkedout += 1;
                    entry.Available -= 1;
                    db.Book.Update(entry);
                }
            }

            

            AspNetUsers tempUser = new AspNetUsers();

            if (User.Identity.Name != null)
            {
                foreach (AspNetUsers user in db.AspNetUsers)
                {
                    if (User.Identity.Name == user.Email)
                    {
                        tempUser = user;
                    }
                }
            }

            DateTime DueDate = new DateTime();
            DueDate = DueDate.AddDays(14);

            db.Transaction.Update(new Transaction()
            {
                Userid = tempUser.Id,
                Booktitle = tempBook.Id,
                Duedate = DueDate,
                Renew = "false"
            });

            return View(tempBook);
        }

        [Authorize]
        public IActionResult Return()
        {
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
