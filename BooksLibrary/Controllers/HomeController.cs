using BooksLibrary.Data.Models;
using BooksLibrary.Data.Repos;
using BooksLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BooksRepo _booksRepo;
        private readonly ReservationsRepo _reservationsRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, BooksRepo booksRepo, ReservationsRepo reservationsRepo, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _booksRepo = booksRepo;
            _reservationsRepo = reservationsRepo;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userReservations = await _reservationsRepo.GetUserReservations(_userManager.GetUserId(User));
            List<Book> userBooks = new List<Book>();
            foreach (var reservation in userReservations)
            {
                var book = await _booksRepo.Get(reservation.Book);
                userBooks.Add(book);
            }

            ViewData["Books"] = userBooks;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BooksList()
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
