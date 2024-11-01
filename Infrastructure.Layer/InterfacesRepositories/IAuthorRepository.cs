using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.InterfacesRepositories
{
    public interface IAuthorRepository
    {
        Task AddAuthor(Author author);
        Task<IEnumerable<Author>> GetAllAuthors();
        Task SaveChanges();
    }
}
