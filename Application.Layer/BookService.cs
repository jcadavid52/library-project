using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.Interfaces;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public BookService(IBookRepository bookRepository,IConfiguration config)
        {
            _bookRepository = bookRepository;
            _config = config;
        }
        public async Task AddBook(AddBookDto addUserDto, string pathGlobalImage)
        {
            

            if (addUserDto.PageNumber < 49)
            {
                throw new ValidationException("La página del libro no puede ser menor a 49 páginas según la UNESCO");
            }

            var resultBook = await _bookRepository.GetBookByCodeReference(addUserDto.CodeReference);

            if(resultBook != null)
            {
                throw new InvalidOperationException($"El código de referencia '{addUserDto.CodeReference}' ya existe");
            }


            var rootFile = UploadFile(addUserDto.FormFile);


            var book = new Book
            {
                Title = addUserDto.Title,
                PageNumber = addUserDto.PageNumber,
                Description = addUserDto.Description,
                IdAuthor = addUserDto.IdAuthor,
                IdCategory = addUserDto.IdCategory,
                DateCreation = DateTime.Now,
                DatePublication = addUserDto.DatePublication,
                CodeReference = addUserDto.CodeReference,
                ReservationId = null,
                PathImage = rootFile,
                PathGlobalImage = pathGlobalImage + "/imagesBooks/" + addUserDto.FormFile.FileName + "_" + Guid.NewGuid(),

            };

            await _bookRepository.AddBook(book);

        }

        public async Task DeleteBook(int id)
        {
            var book = await _bookRepository.GetBook(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un registro con el ID: {id}");
            }

           
            await _bookRepository.DeleteBook(book);
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

        public async Task UpdateBook(UpdateBookDto bookDto, int id)
        {
            var book = await _bookRepository.GetBook(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró un libro con el ID '{id}'");
            }

            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Id = id;
            book.IdAuthor = bookDto.IdAuthor;
            book.IdCategory = bookDto.IdCategory;
            book.PageNumber = bookDto.PageNumber;
            book.DatePublication = bookDto.DatePublication;

            await _bookRepository.UpdateBook();
        }

        public string UploadFile(IFormFile formFile)
        {
            var rootFile =  @_config["FileStorage:UploadPath"] + Guid.NewGuid() + formFile.FileName;
            var directoryUbication = Path.Combine(Directory.GetCurrentDirectory(), rootFile);

            FileInfo file = new FileInfo(directoryUbication);

            if (file.Exists)
            {
                file.Delete();
            }

            using (var fileStream = new FileStream(directoryUbication, FileMode.Create))
            {
               formFile.CopyTo(fileStream);
            }

            return rootFile;

        }
    }
}
