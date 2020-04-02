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
            return View();
        }

        [Authorize]
        public IActionResult Menu(string menu)
        {
            if (menu == null)
            {
                return View();
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
                    foreach (Transaction check in checkedOut)
                    {
                        foreach (Book book in db.Book)
                        {
                            if (check.Booktitle == book.Id)
                            {
                                if (check.Duedate > DateTime.Now)
                                {
                                    TempData["Date"] = check.Duedate.ToShortDateString();
                                    userOut.Add(book);
                                }
                                else
                                {
                                    TempData["Message"] = "Book Overdue!";
                                    userOut.Add(book);
                                }

                            }
                        }
                    }
                    return View("Return", userOut);
                }

                else
                {
                    return View("Return");
                }
            }
        }

        public IActionResult Browse()
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
                Userid = currentUser.Id,
                Booktitle = tempBook.Id,
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
                if (check.Userid == currentUser.Id)
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
                if (check.Booktitle == tempBook.Id && check.Userid == currentUser.Id)
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
                        if (check.Booktitle == books.Id)
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
