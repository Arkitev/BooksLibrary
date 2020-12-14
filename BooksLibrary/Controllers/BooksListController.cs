using BooksLibrary.Data.Models;
using BooksLibrary.Data.Repos;
using BooksLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Controllers
{
    public class BooksListController : Controller
    {
        private readonly BooksRepo _booksRepo;
        private readonly ReservationsRepo _reservationsRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public BooksListController(BooksRepo booksRepo, ReservationsRepo reservationsRepo, UserManager<IdentityUser> userManager)
        {
            _booksRepo = booksRepo;
            _reservationsRepo = reservationsRepo;
            _userManager = userManager;
        }

        public ViewResult BooksList()
        {
            return View();
        }

        public async Task<ActionResult> Index()
        {
            try
            {
                var books = await _booksRepo.GetAll();
                ViewData["Books"] = books;

                return View(books);
            }
            catch
            {
                return View();
            } 
        }

        public async Task<ActionResult> Reservations(Guid id)
        {
            var selectedBook = await _booksRepo.Get(id);
            var bookReservations = await _reservationsRepo.GetBookReservations(id);
            List<ReservationModel> bookReservationsModel = new List<ReservationModel>();
            
            foreach (var reservation in bookReservations)
            {
                var owner = await _userManager.FindByIdAsync(reservation.Owner);
                bookReservationsModel.Add(new ReservationModel()
                {
                    BookTitle = selectedBook.Title,
                    Owner = owner.UserName,
                    Date = reservation.Date
                });
            }

            ViewData["BookReservations"] = bookReservationsModel;

            return View(selectedBook);
        }

        public async Task<ActionResult> Reserve(Guid id)
        {
            try
            {
                var userReservations = await _reservationsRepo.GetUserReservations(_userManager.GetUserId(User));

                if (!userReservations.Exists(reservation => reservation.Book == id))
                {
                    await _reservationsRepo.Add(new Reservation()
                    {
                        Id = new Guid(),
                        Date = DateTime.Now,
                        Owner = _userManager.GetUserId(User),
                        Book = id
                    });
                }
            }
            catch
            {

            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection form)
        {
            try
            {
                await _booksRepo.Add(new Book()
                {
                    Id = new Guid(),
                    Title = form["Title"],
                    Author = form["Author"],
                    ReleaseDate = Convert.ToDateTime(form["ReleaseDate"]),
                    Description = form["Description"]
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var book = await _booksRepo.Get(id);

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, IFormCollection form)
        {
            try
            {
                await _booksRepo.Update(new Book()
                {
                    Id = id,
                    Title = form["Title"],
                    Author = form["Author"],
                    ReleaseDate = Convert.ToDateTime(form["ReleaseDate"]),
                    Description = form["Description"]
                });

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            await _booksRepo.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
