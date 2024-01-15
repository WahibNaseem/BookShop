using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using BookShop.Domain.Services;
using Moq;
using Xunit;

namespace BookShop.Domain.Tests
{

    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _bookService = new BookService(_bookRepositoryMock.Object);
        }
        [Fact]
        public async void GetAll_ShouldReturnAListOfBook_WhenBooksExist()
        {
            var books = CreateBookList();

            _bookRepositoryMock.Setup(x => x.GetAllAsync())
                                                       .ReturnsAsync(books);

            var result = await _bookService.GetAllAsync();

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenBookDoNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetAllAsync())
                                                         .ReturnsAsync((List<Book>) null);

            var result = await _bookService.GetAllAsync();

            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _bookRepositoryMock.Setup(x => x.GetAllAsync())
                                                        .ReturnsAsync(new List<Book>());

            await _bookService.GetAllAsync();

            _bookRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnBook_WhenBookExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x => x.GetByIdAsync(book.Id))
                                                        .ReturnsAsync(book);

            var result = await _bookService.GetByIdAsync(book.Id);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetByIdAsync(1))
                                                        .ReturnsAsync((Book) null);

            var result = await _bookService.GetByIdAsync(1);

            Assert.Null(result);
        }

        [Fact]
        public async void GetById_ShouldCallByRepository_OnlyOnce()
        {
            _bookRepositoryMock.Setup(x => x.GetByIdAsync(1))
                                                       .ReturnsAsync(new Book());

            await _bookService.GetByIdAsync(1);

            _bookRepositoryMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async void Add_ShouldAddBook_WhenBookNameDoesNotExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x =>
                               x.SearchAsync(b => b.Name == book.Name))
                                  .ReturnsAsync(new List<Book>());

            _bookRepositoryMock.Setup(x => x.AddAsync(book));

            var result = await _bookService.AddAsync(book);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);

        }

        [Fact]
        public async void Add_ShouldNotAddBook_WhenBookNameAlreadyExist()
        {
            var book = CreateBook();
            var books = new List<Book>() { book };

            _bookRepositoryMock.Setup(x =>
                              x.SearchAsync(b => b.Name == book.Name))
                                   .ReturnsAsync(books);

            var result = await _bookService.AddAsync(book);

            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldUpdateBook_WhenBookNameDoesNotExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x =>
                                x.SearchAsync(x => x.Name == book.Name && x.Id != book.Id))
                                .ReturnsAsync(new List<Book>());

            _bookRepositoryMock.Setup(x => x.UpdateAsync(book));

            var result = await _bookService.UpdateAsync(book);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);
        }

        [Fact]
        public async void Update_ShouldNotUpdateBook_WhenBookDoesNotExist()
        {
            var book = CreateBook();
            var books = new List<Book>() { book };

            _bookRepositoryMock.Setup(x =>
                                x.SearchAsync(b => b.Name == book.Name && b.Id != book.Id))
                                .ReturnsAsync(books);

            var result = await _bookService.UpdateAsync(book);

            Assert.Null(result);
        }

        [Fact]
        public async void Remove_ShouldReturnTrue_WhenBookCanBeRemoved()
        {
            var book = CreateBook();

            var result = await _bookService.RemoveAsync(book);

            Assert.True(result);
        }

        [Fact]
        public async void GetBooksByCategory_ShouldReturnListOfBook_WhenBooksWithSearchCategoryExist()
        {
            var books = CreateBookList();

            _bookRepositoryMock.Setup(x => x.GetBooksByCategoryAsync(2))
                                                        .ReturnsAsync(books);

            var result = await _bookService.GetBooksByCategoryAsync(2);

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetBooksByCategory_ShouldReturnListOfBook_WhenBooksWithSearchCategoryDoNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetBooksByCategoryAsync(2))
                                                           .ReturnsAsync((IEnumerable<Book>) null);

            var result = await _bookService.GetBooksByCategoryAsync(2);

            Assert.Null(result);
        }

        private Book CreateBook()
        {
            return new Book
            {
                Id = 1,
                Name = "Book Test 1",
                Author = "Author Test 1",
                Description = "Description Test 1",
                CategoryId = 1
            };
        }

        private List<Book> CreateBookList()
        {
            return new List<Book>()
            {
                new Book
                {
                    Id = 1,
                    Name = "Book Test 1",
                    Author = "Author Test 1",
                    Description = "Description Test 1",
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Name = "Book Test 2",
                    Author = "Author Test 2",
                    Description = "Description Test 2",
                    CategoryId = 1
                },
                new Book
                {
                    Id = 3,
                    Name = "Book Test 3",
                    Author = "Author Test 3",
                    Description = "Description Test 3",
                    CategoryId = 2
                }
            };
        }
    }

}