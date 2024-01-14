using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookShopDbContext context) : base(context) { }

        public async Task<List<Book>> GetAll()
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .OrderBy(b => b.Name)
                             .ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .Where(b => b.Id == id)
                             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await Search(b => b.CategoryId == categoryId);
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .Where(b => b.Name.Contains(searchedValue) ||
                                     b.Author.Contains(searchedValue) ||
                                     b.Description.Contains(searchedValue) ||
                                     b.Category.Name.Contains(searchedValue))
                             .ToListAsync();
        }
    }
}