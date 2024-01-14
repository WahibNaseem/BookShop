using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Infrastructure.Data;

namespace BookShop.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookShopDbContext bookShopDb) : base(bookShopDb) { }

    }
}