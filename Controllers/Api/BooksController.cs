using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using LibApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly IMapper _mapper;

        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _booksService = new BooksService(context);
            _mapper = mapper;
        }


        // GET api/books/
        [HttpGet("all")]
        public IEnumerable<BookDto> GetAllBooks()
        {
            var booksQuery = _booksService.GetBooks();

            return booksQuery.Select(_mapper.Map<Book, BookDto>);
        }

        // GET api/books/
        [HttpGet("books")]
        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var booksQuery = _booksService.GetAvailableBooks();

            if (!String.IsNullOrWhiteSpace(query))
            {
                booksQuery = _booksService.GetByName(query);
            }

            return booksQuery.Select(_mapper.Map<Book, BookDto>);
        }


        // GET api/books/
        [HttpGet("{id}")]
        public BookDto GetBooksById(int id)
        {
            var book = _booksService.GetBookById(id);

            var result = _mapper.Map<BookDto>(book);

            return result;
        }

        // DELETE /api/customers
        [HttpDelete("{id}")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _booksService.GetBookById(id);

            _booksService.Delete(id);
        }

    }
}
