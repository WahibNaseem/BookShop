using BookShop.Domain.Common;

namespace BookShop.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }

        /* EF Relations */
        public ICollection<Book> Books { get; set; }
    }
}