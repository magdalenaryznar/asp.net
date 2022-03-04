using Microsoft.AspNetCore.Mvc;
using System;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Services;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private BooksService _booksService;
        public BooksController(ApplicationDbContext context)
        {
            _booksService = new BooksService(context);
        }

        public IActionResult Index()
        {
            var books = _booksService.GetBooks();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _booksService.GetBookById(id);

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _booksService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _booksService.GetGenres()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var genres = _booksService.GetGenres();

            var viewModel = new BookFormViewModel
            {
                Genres = genres,
                Book = new Book()
                {
                    Id = 0,
                 }
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        public IActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookFormViewModel
                {
                    Book = book,
                    Genres = _booksService.GetGenres()
                };
                return View("BookForm", viewModel);
            }
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _booksService.Create(book);
            }
            else
            {
                _booksService.Edit(book.Id, book);
            }


            return RedirectToAction("Index", "Books");
        }
    }
}
