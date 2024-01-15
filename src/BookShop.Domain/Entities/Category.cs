using BookShop.Domain.Common;

namespace BookShop.Domain.Entities
{
    /// <summary>
    /// Declare a class to make the record of the category
    /// </summary>
    public class Category : BaseAuditableEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        /// <summary>
        /// Name of the category, like History, Information technologies
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Each category can have number of books
        /// and it is for realtionship between 2 table
        /// </summary>
        /* EF Relations */
        public ICollection<Book> Books { get; set; }
    }
}