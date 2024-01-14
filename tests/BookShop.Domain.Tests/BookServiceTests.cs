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

            _bookRepositoryMock.Setup(x => x.GetAll())
                                                       .ReturnsAsync(books);

            var result = await _bookService.GetAll();

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnNull_WhenBookDoNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetAll())
                                                         .ReturnsAsync((List<Book>) null);

            var result = await _bookService.GetAll();

            Assert.Null(result);
        }

        [Fact]
        public async void GetAll_ShouldCallGetAllFromRepository_OnlyOnce()
        {
            _bookRepositoryMock.Setup(x => x.GetAll())
                                                        .ReturnsAsync(new List<Book>());

            await _bookService.GetAll();

            _bookRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnBook_WhenBookExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x => x.GetById(book.Id))
                                                        .ReturnsAsync(book);

            var result = await _bookService.GetById(book.Id);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetById(1))
                                                        .ReturnsAsync((Book) null);

            var result = await _bookService.GetById(1);

            Assert.Null(result);
        }

        [Fact]
        public async void GetById_ShouldCallByRepository_OnlyOnce()
        {
            _bookRepositoryMock.Setup(x => x.GetById(1))
                                                       .ReturnsAsync(new Book());

            await _bookService.GetById(1);

            _bookRepositoryMock.Verify(x => x.GetById(1), Times.Once);
        }

        [Fact]
        public async void Add_ShouldAddBook_WhenBookNameDoesNotExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x =>
                               x.Search(b => b.Name == book.Name))
                                  .ReturnsAsync(new List<Book>());

            _bookRepositoryMock.Setup(x => x.AddAsync(book));

            var result = await _bookService.Add(book);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);

        }

        [Fact]
        public async void Add_ShouldNotAddBook_WhenBookNameAlreadyExist()
        {
            var book = CreateBook();
            var bookList = new List<Book>() { book };

            _bookRepositoryMock.Setup(x =>
                              x.Search(b => b.Name == book.Name))
                                   .ReturnsAsync(bookList);

            var result = await _bookService.Add(book);

            Assert.Null(result);
        }

        [Fact]
        public async void Update_ShouldUpdateBook_WhenBookNameDoesNotExist()
        {
            var book = CreateBook();

            _bookRepositoryMock.Setup(x =>
                                x.Search(x => x.Name == book.Name && x.Id != book.Id))
                                .ReturnsAsync(new List<Book>());

            _bookRepositoryMock.Setup(x => x.UpdateAsync(book));

            var result = await _bookService.Update(book);

            Assert.NotNull(result);
            Assert.IsType<Book>(result);
        }

        [Fact]
        public async void Update_ShouldNotUpdateBook_WhenBookDoesNotExist()
        {
            var book = CreateBook();
            var bookList = new List<Book>() { book };

            _bookRepositoryMock.Setup(x =>
                                x.Search(b => b.Name == book.Name && b.Id != book.Id))
                                .ReturnsAsync(bookList);

            var result = await _bookService.Update(book);

            Assert.Null(result);
        }

        [Fact]
        public async void Remove_ShouldReturnTrue_WhenBookCanBeRemoved()
        {
            var book = CreateBook();

            var result = await _bookService.Remove(book);

            Assert.True(result);
        }

        [Fact]
        public async void GetBooksByCategory_ShouldReturnListOfBook_WhenBooksWithSearchCategoryExist()
        {
            var books = CreateBookList();

            _bookRepositoryMock.Setup(x => x.GetBooksByCategory(2))
                                                        .ReturnsAsync(books);

            var result = await _bookService.GetBooksByCategory(2);

            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
        }

        [Fact]
        public async void GetBooksByCategory_ShouldReturnListOfBook_WhenBooksWithSearchCategoryDoNotExist()
        {
            _bookRepositoryMock.Setup(x => x.GetBooksByCategory(2))
                                                           .ReturnsAsync((IEnumerable<Book>) null);

            var result = await _bookService.GetBooksByCategory(2);

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