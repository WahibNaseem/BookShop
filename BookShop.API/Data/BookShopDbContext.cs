using BookShop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShop.API.Data
{
    public class BookShopDbContext : DbContext
    {
        public BookShopDbContext(DbContextOptions<BookShopDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
