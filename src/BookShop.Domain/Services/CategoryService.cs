using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;

namespace BookShop.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IBookService _bookService;

        public CategoryService(ICategoryRepository categoryRepository, IBookService bookService)
        {
            _categoryRepository = categoryRepository;
            _bookService = bookService;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> AddAsync(Category category)
        {
            if (_categoryRepository.SearchAsync(c => c.Name == category.Name).Result.Any())
                return null;

            await _categoryRepository.AddAsync(category);
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            if (_categoryRepository.SearchAsync(c => c.Name == category.Name && c.Id != category.Id).Result.Any())
                return null;

            await _categoryRepository.UpdateAsync(category);
            return category;
        }

        public async Task<bool> RemoveAsync(Category category)
        {
            var books = await _bookService.GetBooksByCategoryAsync(category.Id);
            if (books.Any()) return false;

            await _categoryRepository.RemoveAsync(category);
            return true;
        }

        public async Task<IEnumerable<Category>> SearchAsync(string categoryName)
        {
            return await _categoryRepository.SearchAsync(c => c.Name.Contains(categoryName));
        }

        public void Dispose()
        {
            _categoryRepository?.Dispose();
        }

    }
}