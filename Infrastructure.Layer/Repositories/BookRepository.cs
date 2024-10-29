﻿using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDBContext _libraryDbContext;
        public BookRepository(LibraryDBContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public async Task AddBook(Book book)
        {
            await _libraryDbContext.Books.AddAsync(book);
            await _libraryDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _libraryDbContext.Books.Include("Author").Include("Category").ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _libraryDbContext.SaveChangesAsync();
        }
    }
}
