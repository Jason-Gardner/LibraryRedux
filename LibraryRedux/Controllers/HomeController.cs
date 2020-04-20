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

        // Initial Index Action
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        // Browse the Library
        [Authorize]
        public IActionResult Browse()
        {
            return View();
        }

        // Search Action and Search functionality
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

        // Admin Actions for Admin Account
        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize]
        public IActionResult ManageUser()
        {
            return View();
        }

        public IActionResult ManageBook()
        {
            return View();
        }

        public IActionResult Update()
        {
            return View("Update");
        }

        public IActionResult ManageUsers(string user)
        {
            return View("ManageUser", FindUser(user));
        }

        public IActionResult deleteUser(string currentUser)
        {
            LibraryDBContext db = new LibraryDBContext();

            db.AspNetUsers.Remove(FindUser(currentUser));
            db.SaveChanges();

            return View("Admin");
        }

        public IActionResult ManageBooks(string currentBook)
        {
            return View("ManageBook", FindBook(currentBook));
        }

        public IActionResult SelectManageBook(string currentBook)
        {
            return View("SelectManageBook", FindBook(currentBook));
        }

        public IActionResult addBook(string Title, string Author, string Genre, int Available, string Type)
        {
            LibraryDBContext db = new LibraryDBContext();
            Book newBook = new Book();

            newBook.Title = Title;
            newBook.Author = Author;
            newBook.Genre = Genre;
            newBook.Available = Available;
            newBook.Checkedout = 0;
            newBook.Type = Type;

            db.Book.Add(newBook);
            db.SaveChanges();

            return View("Manage");
        }

        public IActionResult UpdateBook(int id, string title, string author, int avail, string genre)
        {
            LibraryDBContext db = new LibraryDBContext();
            Book tbook = new Book();

            foreach(Book book in db.Book)
            {
                if (id == book.Id)
                {
                    tbook = book;
                }
            }

            tbook.Title = title;
            tbook.Author = author;
            tbook.Available = avail;
            tbook.Genre = genre;

            db.Book.Update(tbook);
            db.SaveChanges();

            return View("ManageBook");
        }

        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }

        // Back-end Functions including making sure we have the current user, refactored user search
        public AspNetUsers FindUser(string user)
        {
            LibraryDBContext db = new LibraryDBContext();
            List<AspNetUsers> userList = db.AspNetUsers.ToList<AspNetUsers>();
            AspNetUsers selected = new AspNetUsers();

            if (user != null)
            {
                foreach (AspNetUsers logons in userList)
                {
                    if (logons.UserName == user)
                    {
                        selected = logons;

                    }
                }
            }
            return selected;
        }

        public Book FindBook(string currentBook)
        {
            LibraryDBContext db = new LibraryDBContext();
            Book viewBook = new Book();

            foreach (Book book in db.Book)
            {
                if (book.Title == currentBook)
                {
                    viewBook = book;
                }
            }

            return viewBook;
        }

        public List<Book> UpdateLibrary(List<Book> library)
        {
            LibraryDBContext db = new LibraryDBContext();
            library = db.Book.ToList<Book>();
            return library;
        }

        public AspNetUsers FindCurrentUser()
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

        // Main Menu Action - Funnels selection from Index view into correct Action/View
        [Authorize]
        public IActionResult Menu(string menu)
        {
            if (menu == null)
            {
                return View();
            }
            if (menu == "manage")
            {
                return View("Admin");
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

        // User Functions

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

            AspNetUsers currentUser = FindCurrentUser();

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

            AspNetUsers currentUser = FindCurrentUser();

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

            AspNetUsers currentUser = FindCurrentUser();

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

        // Methods from creation of application
        public IActionResult Privacy()
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
