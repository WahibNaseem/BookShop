using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookShopDbContext context) : base(context) { }

        /// <summary>
        /// while communicate with database it gets all the available book 
        /// </summary>
        /// <returns>return the books from the database</returns>
        public override async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .OrderBy(b => b.Name)
                             .ToListAsync();
        }

        /// <summary>
        ///  it communicate with database to get a single book through the id of that book
        ///  and it's respective property through Eager Loading
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<Book> GetByIdAsync(int id)
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .Where(b => b.Id == id)
                             .FirstOrDefaultAsync();
        }

        /// <summary>
        /// it communicate with database to search the book through its name
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>from database It will return the books</returns>
        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await SearchAsync(b => b.CategoryId == categoryId);
        }

        /// <summary>
        /// it communicate with database to search the book with its  name , author, category
        /// and return it respective property through eager loading
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>from the database It will return  books available whatever search key we pass</returns>
        public async Task<IEnumerable<Book>> SearchBookWithCategoryAsync(string searchKey)
        {
            return await BookShopDb.Books.AsNoTracking()
                             .Include(b => b.Category)
                             .Where(b => b.Name.Contains(searchKey) ||
                                     b.Author.Contains(searchKey) ||
                                     b.Description.Contains(searchKey) ||
                                     b.Category.Name.Contains(searchKey))
                             .ToListAsync();
        }
    }
}