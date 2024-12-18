﻿using Application.Layer.DTOs;
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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddCategory(AddCategoryDto categoryDto)
        {
            var categoryResult = await _categoryRepository.GetCategoryByName(categoryDto.Name);

            if(categoryResult != null)
            {
                throw new KeyNotFoundException($"La categoría con el nombre '{categoryDto.Name}' ya existe");
            }

            var category = new Category
            {
                Name = categoryDto.Name,
            };

            await _categoryRepository.AddCategory(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var catogories = await _categoryRepository.GetAllCategories();
            var catogoriesDto = new List<CategoryDto>();

            foreach (var category in catogories)
            {
                var catogorieDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                };

                catogoriesDto.Add(catogorieDto);
            }

            return catogoriesDto;

        }
    }
}
