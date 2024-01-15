using BookShop.Domain.Entities;

namespace BookShop.Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);

        Task<IEnumerable<Book>> SearchBookWithCategoryAsync(string searchKeys);

    }
}