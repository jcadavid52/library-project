using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (addUserDto.PageNumber < 49)
            {
                throw new ValidationException("La página del libro no puede ser menor a 49 páginas según la UNESCO");
            }

            var resultBook = GetBookByTitle(addUserDto.Title);

            if(resultBook != null)
            {
                throw new InvalidOperationException($"El título '{addUserDto.Title}' ya existe");
            }

            var book = new Book
            {
                Title = addUserDto.Title,
                PageNumber = addUserDto.PageNumber,
                Description = addUserDto.Description,
                IdAuthor = addUserDto.IdAuthor,
                IdCategory = addUserDto.IdCategory,
                DateCreation = DateTime.Now,
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
                    Author = new AuthorDto
                    {
                        Id = book.Author.Id,
                        Name = book.Author.Name,
                        Nationality = book.Author.Nationality,
                        Birthdate = book.Author.Birthdate,
                    },
                    Genero = new GeneroDto
                    {
                        Id = book.Category.Id,
                        Name = book.Category.Name,
                    }

                };

                listBooksDtos.Add(bookDto);
               
            }

            return listBooksDtos;
            
        }

        public async Task<BookDto> GetBook(int id)
        {
            var book = await _bookRepository.GetBook(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID '{id}'");
            }


            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                PageNumber = book.PageNumber,
                Description = book.Description,
                DatePublication = book.DatePublication,
                DateCreation = book.DateCreation,
                Author = new AuthorDto
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Nationality = book.Author.Nationality,
                    Birthdate = book.Author.Birthdate,
                },
                Genero = new GeneroDto
                {
                    Id = book.Category.Id,
                    Name = book.Category.Name,
                }
            };

            return bookDto;
        }

        public async Task<BookDto> GetBookByTitle(string title)
        {
            var book = await _bookRepository.GetBookByTitle(title);

            if(book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el título '{title}'");
            }

            var bookDto = new BookDto
            {
                Id= book.Id,
                Title = book.Title,
                PageNumber = book.PageNumber,
                Description = book.Description,
                DatePublication = book.DatePublication,
                DateCreation = book.DateCreation,
                Author = new AuthorDto
                {
                    Id = book.Author.Id,
                    Name = book.Author.Name,
                    Nationality = book.Author.Nationality,
                    Birthdate = book.Author.Birthdate,
                },
                Genero= new GeneroDto
                {
                    Id = book.Category.Id,
                    Name = book.Category.Name,
                }
            };

            return bookDto;
        }
    }
}
