using Microsoft.AspNetCore.Mvc;
using System;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using LibApp.Services;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private BooksService _booksService;
        private IHttpContextAccessor _httpContextAccessor;
        public BooksController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _booksService = new BooksService(context);
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var books = _booksService.GetBooks();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = GetBookFromApi(id).Result;

            return View(book);
        }

        [Authorize(Roles = "Owner,StoreManager")]
        public IActionResult Edit(int id)
        {
            var book = GetBookFromApi(id).Result;
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

        [Authorize(Roles = "Owner,StoreManager")]
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
        [Authorize(Roles = "Owner,StoreManager")]
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

        private async Task<Book> GetBookFromApi(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
                var result = await httpClient.GetAsync($"{apiUrl}/api/books/{id}");

                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var content = await result.Content.ReadAsStringAsync();

                var book = JsonConvert.DeserializeObject<Book>(content);

                return book;
            }
        }
    }
}