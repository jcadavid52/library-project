﻿using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.InterfacesRepositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetBook(int id);
        Task AddBook(Book book);
        Task UpdateBook();
        Task DeleteBook(Book book);
        Task<Book> GetBookByTitle(string title);
        Task<Book> GetBookByCodeReference(string codeReference);
        Task<IEnumerable<Book>> GetBooksByCategory(int IdCategory);
        Task SaveChanges();
    }
}
