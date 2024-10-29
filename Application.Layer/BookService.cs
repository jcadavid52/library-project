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
    }
}
