﻿using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Services
{
    public class BooksService
    {
        private readonly ApplicationDbContext _context;

        public BooksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks()
        {
            return _context.Books
               .Include(b => b.Genre)
               .ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books
               .Include(b => b.Genre)
               .FirstOrDefault(c=>c.Id == id);
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genre.ToList();
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Edit(int id, Book book)
        {
            var bookInDb = _context.Books.Include(b => b.Genre).Single(c=> c.Id == id);
            bookInDb.Name = book.Name;
            bookInDb.AuthorName = book.AuthorName;
            bookInDb.GenreId = book.GenreId;
            bookInDb.ReleaseDate = book.ReleaseDate;
            bookInDb.DateAdded = book.DateAdded;
            bookInDb.NumberInStock = book.NumberInStock;

            _context.SaveChanges();
        }
    }
}
