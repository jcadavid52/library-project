using Infrastructure.Layer.Models;
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
        Task DeleteBook(int id);
        Task<Book> GetBookByTitle(string title);
        Task SaveChanges();
    }
}
