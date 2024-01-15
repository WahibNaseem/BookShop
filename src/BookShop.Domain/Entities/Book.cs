using BookShop.Domain.Common;

namespace BookShop.Domain.Entities
{
    /// <summary>
    /// make the class to have to records of book in system
    /// </summary>
    public class Book : BaseAuditableEntity
    {
        /// <summary>
        /// Name of the Book  
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Name of the Author of the Book
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Description of the book
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Publish date of the book
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// Category id of the book , like Science , History
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Use Category to make the relationship between book and category
        /// </summary>
        /* EF Relation */
        public Category Category { get; set; }
    }


}