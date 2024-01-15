using BookShop.Domain.Entities;

namespace BookShop.Domain.Interfaces
{
    /// <summary>
    ///   it is design to provide service to application for the  required queries 
    ///   with the help of system
    /// </summary>
    public interface IBookService : IDisposable
    {
        /// <summary>
        ///  it  gets you all the books available in the system
        /// </summary>
        /// <returns> it will return books from the sytem</returns>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// it helps you to get a single book through the id of that book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>it will return a book of specific id you pass if that book exists</returns>
        Task<Book> GetByIdAsync(int id);

        /// <summary>
        ///  it helps you add the new book into the system if it doesn't exist
        /// </summary>
        /// <param name="book"></param>
        /// <returns>it will return a book which has been added in the system</returns>
        Task<Book> AddAsync(Book book);

        /// <summary>
        /// it hleps you to update the book into the system if that exist
        /// </summary>
        /// <param name="book"></param>
        /// <returns>it will return the book which has been updated successfully</returns>
        Task<Book> UpdateAsync(Book book);

        /// <summary>
        /// it helps you to remove the book from the system if that exist
        /// </summary>
        /// <param name="book"></param>
        /// <returns>if the book been removed successfully it will true otherwise it will return false</returns>
        Task<bool> RemoveAsync(Book book);

        /// <summary>
        ///  it helps you to search the books through its category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>it will return the books of specific category if that category exists</returns>
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId);

        /// <summary>
        ///  it helps you search the book through its name
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns>It will return the books</returns>
        Task<IEnumerable<Book>> SearchAsync(string bookName);

        /// <summary>
        ///  search the book with its  name , author, category
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>It will return  books available whatever search key we pass</returns>
        Task<IEnumerable<Book>> SearchBookWithCategoryAsync(string searchKey);

    }
}