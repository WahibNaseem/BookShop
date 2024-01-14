
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;

namespace BookShop.Domain.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _bookRepository.GetAll();
        }

        public async Task<Book> GetById(int id)
        {
            return await _bookRepository.GetById(id);
        }
        public async Task<Book> Add(Book book)
        {
            if (_bookRepository.Search(b => b.Name == book.Name).Result.Any())
                return null;

            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task<Book> Update(Book book)
        {
            if (_bookRepository.Search(b => b.Name == book.Name && b.Id != book.Id).Result.Any())
                return null;

            await _bookRepository.UpdateAsync(book);
            return book;
        }

        public async Task<bool> Remove(Book book)
        {
            await _bookRepository.RemoveAsync(book);
            return true;
        }

        public async Task<IEnumerable<Book>> GetBooksByCategory(int categoryId)
        {
            return await _bookRepository.GetBooksByCategory(categoryId);
        }

        public async Task<IEnumerable<Book>> Search(string bookName)
        {
            return await _bookRepository.Search(c => c.Name.Contains(bookName));
        }

        public async Task<IEnumerable<Book>> SearchBookWithCategory(string searchedValue)
        {
            return await _bookRepository.SearchBookWithCategory(searchedValue);
        }

        public void Dispose()
        {
            _bookRepository?.Dispose();
        }
    }
}