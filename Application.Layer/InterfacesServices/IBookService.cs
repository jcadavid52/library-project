using Application.Layer.DTOs;

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.InterfacesServices
{
    public interface IBookService
    {
        Task AddBook(AddBookDto addUserDto, string pathGlobalImage);
        Task UpdateBook(UpdateBookDto bookDto,int id);
        Task DeleteBook(int id);
        Task<BookDto> GetBook(int id);
        Task<BookDto> GetBookByTitle(string title);
        Task<IEnumerable<BookDto>> GetAllBooks();
        Task<IEnumerable<BookDto>> GetBooksByCategory(int IdCategory);
        string UploadFile(IFormFile formFile);
        
    }
}
