using Application.Layer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Layer.InterfacesServices
{
    public interface IAuthorService
    {
        Task AddAuthor(AddAuthorDto authorDto);
        Task<IEnumerable<AuthorDto>> GetAllAuthors();
    }
}
