using Infrastructure.Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.InterfacesRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task AddCategory(Category category);
        Task<Category> GetCategoryByName(string name);
        Task SaveChanges();


    }
}
