
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;

namespace BookShop.Domain.Services
{
    /// <inheritdoc />
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            if (_bookRepository.SearchAsync(b => b.Name == book.Name).Result.Any())
                return null;

            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            if (_bookRepository.SearchAsync(b => b.Name == book.Name && b.Id != book.Id).Result.Any())
                return null;

            await _bookRepository.UpdateAsync(book);
            return book;
        }

        public async Task<bool> RemoveAsync(Book book)
        {
            await _bookRepository.RemoveAsync(book);
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _bookRepository.GetBooksByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Book>> SearchAsync(string bookName)
        {
            return await _bookRepository.SearchAsync(c => c.Name.Contains(bookName));
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategoryAsync(string searchKey)
        {
            return await _bookRepository.SearchBookWithCategoryAsync(searchKey);
        }

        /// <summary>
        /// It can help you destroy repository to clean up  that eventually
        /// talk to the  system database 
        /// </summary>
        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}