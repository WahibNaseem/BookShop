using BookShop.Domain.Entities;

namespace BookShop.Domain.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> RemoveAsync(Category category);
        Task<IEnumerable<Category>> SearchAsync(string categoryName);
    }
}