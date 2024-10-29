using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task AddBook(AddBookDto addUserDto)
        {
            var book = new Book
            {
                Title = addUserDto.Title,
                PageNumber = addUserDto.PageNumber,
                Description = addUserDto.Description,
                IdAuthor = addUserDto.IdAuthor,
                IdCategory = addUserDto.IdCategory,
                DateCreation = addUserDto.DateCreation,
                DatePublication = addUserDto.DatePublication

            };

            await _bookRepository.AddBook(book);

        }

        public async Task DeleteBook(int id)
        {
            await _bookRepository.DeleteBook(id);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooks();

            var listBooksDtos = new List<BookDto>();

            foreach (var book in books)
            {

                var bookDto = new BookDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    PageNumber = book.PageNumber,
                    Description = book.Description,
                    DatePublication = book.DatePublication,
                    DateCreation = book.DateCreation,
                    Author = book.Author.Name,
                    Genero = book.Category.Name

                };

                listBooksDtos.Add(bookDto);
               
            }

            return listBooksDtos;
            
        }
    }
}
