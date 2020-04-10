using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LibraryRedux.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

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
            return View();
        }

        [Authorize]
        public IActionResult Menu(string menu)
        {
            if (menu == null)
            {
                return View();
            }
            if (menu == "manage")
            {
                return View("Manage");
            }
            if (menu == "search")
            {
                return View("Search");
            }
            if (menu == "library")
            {
                return View("Browse");
            }
            else
            {
                List<Transaction> checkedOut = UserBook();
                List<Book> userOut = new List<Book>();
                LibraryDBContext db = new LibraryDBContext();

                if (checkedOut.Count > 0)
                {
                    return View("Return", checkedOut);
                }
                else
                {
                    return View("Return");
                }
            }
        }

        [Authorize]
        public IActionResult Browse()
        {
            return View();
        }

        public IActionResult userManage(string user)
        {
            LibraryDBContext db = new LibraryDBContext();
            List<AspNetUsers> userList = db.AspNetUsers.ToList<AspNetUsers>();
            AspNetUsers selected = new AspNetUsers();

            foreach (AspNetUsers logons in userList)
            {
                if (logons.UserName == user)
                {
                    selected = logons;
                    
                }
            }

            return View("Manage", selected);
        }

        public IActionResult bookManage()
        {
            return View("Manage");
        }

        public IActionResult addBook()
        {
            return View("Update");
        }


        [Authorize]
        public IActionResult Manage()
        {
            return View();
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
                    tempBook.Checkedout += 1;
                    tempBook.Available -= 1;
                    db.Book.Update(tempBook);
                }
            }

            AspNetUsers currentUser = FindUser();

            DateTime DueDate = DateTime.Now;
            DueDate = DueDate.AddDays(14);

            db.Transaction.Update(new Transaction()
            {
                Userid = User.Identity.Name,
                Booktitle = JsonSerializer.Serialize<Book>(tempBook),
                Duedate = DueDate,
                Renew = "false"
            });

            TempData["Check"] = tempBook.Title;
            db.SaveChanges();

            return RedirectToAction("Search");
        }

        [Authorize]
        public List<Transaction> UserBook()
        {
            LibraryDBContext db = new LibraryDBContext();

            AspNetUsers currentUser = FindUser();

            List<Transaction> checkedOut = new List<Transaction>();

            foreach (Transaction check in db.Transaction)
            {
                if (check.Userid == User.Identity.Name)
                {
                    checkedOut.Add(check);
                }
            }

            return checkedOut;
        }

        public IActionResult Return()
        {
            return View();
        }

        public IActionResult ReturnBook(string book)
        {
            LibraryDBContext db = new LibraryDBContext();
            Book tempBook = new Book();
            foreach (Book entry in db.Book)
            {
                if (entry.Title == book)
                {
                    tempBook = entry;
                    tempBook.Checkedout -= 1;
                    tempBook.Available += 1;
                    db.Book.Update(tempBook);
                }
            }

            AspNetUsers currentUser = FindUser();

            foreach (Transaction check in db.Transaction)
            {
                if ((JsonSerializer.Deserialize<Book>(check.Booktitle)).Id == tempBook.Id && check.Userid == currentUser.Id)
                {
                    db.Remove(check);
                }
            }

            db.SaveChanges();

            List<Transaction> checkedOut = UserBook();
            List<Book> userOut = new List<Book>();

            if (checkedOut.Count > 0)
            {
                foreach (Transaction check in checkedOut)
                {
                    foreach (Book books in db.Book)
                    {
                        if ((JsonSerializer.Deserialize<Book>(check.Booktitle)).Id == books.Id)
                        {
                            if (check.Duedate > DateTime.Now)
                            {
                                TempData["Date"] = check.Duedate.ToShortDateString();
                                userOut.Add(books);
                            }
                            else
                            {
                                TempData["Message"] = "Book Overdue!";
                                userOut.Add(books);
                            }

                        }
                    }
                }
                return View("Return", userOut);
            }
            else
            {
                return RedirectToAction("Return");
            }
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

        public AspNetUsers FindUser()
        {
            LibraryDBContext db = new LibraryDBContext();
            AspNetUsers currentUser = new AspNetUsers();

            foreach (AspNetUsers user in db.AspNetUsers)
            {
                if (User.Identity.Name == user.Email)
                {
                    currentUser = user;
                }
            }

            return currentUser;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
