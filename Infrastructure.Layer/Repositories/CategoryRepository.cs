using Infrastructure.Layer.InterfacesRepositories;
using Infrastructure.Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Layer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDBContext _dbContext;

        public CategoryRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddCategory(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await SaveChanges();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
           return  await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
