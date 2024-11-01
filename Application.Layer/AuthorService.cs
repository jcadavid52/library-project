using Application.Layer.DTOs;
using Application.Layer.InterfacesServices;
using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task AddAuthor(AddAuthorDto authorDto)
        {
            var authorResult = await _authorRepository.GetAuthorByName(authorDto.Name);

            if (authorResult != null)
            {
                throw new InvalidOperationException($"El autor con el nombre {authorDto.Name} ya existe");
            }

            if (!CalculateAge(authorDto.Birthdate))
            {
                throw new InvalidOperationException($"El autor debe ser mayor de 18 años");
            }



            var author = new Author
            {
                Name = authorDto.Name,
                Nationality = authorDto.Nationality,
                Birthdate = authorDto.Birthdate,
            };

            await _authorRepository.AddAuthor(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthors()
        {
            var authors = await _authorRepository.GetAllAuthors();

            var listAuthorsDtos = new List<AuthorDto>();

            foreach (var author in authors)
            {
                var authorDto = new AuthorDto
                {
                    Id = author.Id,
                    Name = author.Name,
                    Nationality = author.Nationality,
                    Birthdate = author.Birthdate,
                };

                listAuthorsDtos.Add(authorDto);
            }

            return listAuthorsDtos;
        }

        static bool CalculateAge(DateTime birthDate)
        {
            var yearNow = DateTime.Now.Year;
            var calculateAgeResult = yearNow - birthDate.Year;

            if(calculateAgeResult < 18)
            {
                return false;
            }

            return true;
        }
    }
}
