using AutoMapper;
using BookShop.API.Contracts.V1.Book.Requests;
using BookShop.API.Contracts.V1.Book.Responses;
using BookShop.API.Controllers.V1;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookShop.API.Tests
{

    public class BooksControllerTests
    {
        private readonly BooksController _booksController;
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public BooksControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
            _mapperMock = new Mock<IMapper>();
            _booksController = new BooksController( _bookServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenExistBooks()
        {
            var books = CreateBookList();
            var dtoExpected = MapModelToBookResponse(books);

            _bookServiceMock.Setup(c => c.GetAllAsync()).ReturnsAsync(books);
            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<List<Book>>())).Returns(dtoExpected);

            var result = await _booksController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnOk_WhenDoesNotExistAnyBook()
        {
            var books = new List<Book>();
            var dtoExpected = MapModelToBookResponse(books);

            _bookServiceMock.Setup(c => c.GetAllAsync()).ReturnsAsync(books);
            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponse>>(It.IsAny<List<Book>>())).Returns(dtoExpected);

            var result = await _booksController.GetAll();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnOk_WhenBookExist()
        {
            var book = CreateBook();
            var bookResponse = MapModelToBookResponse(book);

            _bookServiceMock.Setup(c => c.GetByIdAsync(2)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

            var result = await _booksController.GetById(2);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            _bookServiceMock.Setup(c => c.GetByIdAsync(2)).ReturnsAsync((Book) null);

            var result = await _booksController.GetById(2);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetById_ShouldCallGetByIdFromService_OnlyOnce()
        {
            var book = CreateBook();
            var bookResponse = MapModelToBookResponse(book);

            _bookServiceMock.Setup(c => c.GetByIdAsync(2)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

            await _booksController.GetById(2);

            _bookServiceMock.Verify(mock => mock.GetByIdAsync(2), Times.Once);
        }       

        [Fact]
        public async void Add_ShouldReturnOk_WhenBookIsAdded()
        {
            var book = CreateBook();
            var createBookRequest = new CreateBookRequest() { Name = book.Name };
            var bookResponse = MapModelToBookResponse(book);

            _mapperMock.Setup(m => m.Map<Book>(It.IsAny<CreateBookRequest>())).Returns(book);
            _bookServiceMock.Setup(c => c.AddAsync(book)).ReturnsAsync(book);
            _mapperMock.Setup(m => m.Map<BookResponse>(It.IsAny<Book>())).Returns(bookResponse);

            var result = await _booksController.Add(createBookRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var createBookRequest = new CreateBookRequest();
            _booksController.ModelState.AddModelError("Name", "The field name is required");

            var result = await _booksController.Add(createBookRequest);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Add_ShouldReturnBadRequest_WhenBookResultIsNull()
        {
            var book = CreateBook();
            var createBookRequest = new CreateBookRequest() { Name = book.Name };

            _mapperMock.Setup(m => m.Map<Book>(It.IsAny<CreateBookRequest>())).Returns(book);
            _bookServiceMock.Setup(c => c.AddAsync(book)).ReturnsAsync((Book) null);

            var result = await _booksController.Add(createBookRequest);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Add_ShouldCallAddFromService_OnlyOnce()
        {
            var book = CreateBook();
            var createBookRequest = new CreateBookRequest() { Name = book.Name };

            _mapperMock.Setup(m => m.Map<Book>(It.IsAny<CreateBookRequest>())).Returns(book);
            _bookServiceMock.Setup(c => c.AddAsync(book)).ReturnsAsync(book);

            await _booksController.Add(createBookRequest);

            _bookServiceMock.Verify(mock => mock.AddAsync(book), Times.Once);
        }

        [Fact]
        public async void Update_ShouldReturnOk_WhenBookIsUpdatedCorrectly()
        {
            var book = CreateBook();
            var updateBookRequest = new UpdateBookRequest() { Id = book.Id, Name = "Test" };

            _mapperMock.Setup(m => m.Map<Book>(It.IsAny<UpdateBookRequest>())).Returns(book);
            _bookServiceMock.Setup(c => c.GetByIdAsync(book.Id)).ReturnsAsync(book);
            _bookServiceMock.Setup(c => c.UpdateAsync(book)).ReturnsAsync(book);

            var result = await _booksController.Update(updateBookRequest.Id, updateBookRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Update_ShouldReturnBadRequest_WhenBookIdIsDifferentThenParameterId()
        {
            var updateBookReqest = new UpdateBookRequest() { Id = 1, Name = "Test" };

            var result = await _booksController.Update(2, updateBookReqest);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Update_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            var updateBookReqest = new UpdateBookRequest() { Id = 1 };
            _booksController.ModelState.AddModelError("Name", "The field name is required");

            var result = await _booksController.Update(1, updateBookReqest);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void Update_ShouldCallUpdateFromService_OnlyOnce()
        {
            var book = CreateBook();
            var updateBookReqest = new UpdateBookRequest() { Id = book.Id, Name = "Test" };

            _mapperMock.Setup(m => m.Map<Book>(It.IsAny<UpdateBookRequest>())).Returns(book);
            _bookServiceMock.Setup(c => c.GetByIdAsync(book.Id)).ReturnsAsync(book);
            _bookServiceMock.Setup(c => c.UpdateAsync(book)).ReturnsAsync(book);

            await _booksController.Update(updateBookReqest.Id, updateBookReqest);

            _bookServiceMock.Verify(mock => mock.UpdateAsync(book), Times.Once);
        }

        [Fact]
        public async void Remove_ShouldReturnOk_WhenBookIsRemoved()
        {
            var book = CreateBook();
            _bookServiceMock.Setup(c => c.GetByIdAsync(book.Id)).ReturnsAsync(book);
            _bookServiceMock.Setup(c => c.RemoveAsync(book)).ReturnsAsync(true);

            var result = await _booksController.Remove(book.Id);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void Remove_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            var book = CreateBook();
            _bookServiceMock.Setup(c => c.GetByIdAsync(book.Id)).ReturnsAsync((Book) null);

            var result = await _booksController.Remove(book.Id);

            Assert.IsType<NotFoundResult>(result);
        }

        private Book CreateBook()
        {
            return new Book()
            {
                Id = 2,
                Name = "Book Test",
                Author = "Author Test",
                Description = "Description Test",
                CategoryId = 1,
                PublishDate = DateTime.MinValue.AddYears(40),
                Category = new Category()
                {
                    Id = 1,
                    Name = "Category Test"
                }
            };
        }

        private BookResponse MapModelToBookResponse(Book book)
        {
            var bookDto = new BookResponse()
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                Description = book.Description,
                PublishDate = book.PublishDate,
                CategoryId = book.CategoryId
            };
            return bookDto;
        }

        private List<Book> CreateBookList()
        {
            return new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "Book Test 1",
                    Author = "Author Test 1",
                    Description = "Description Test 1",
                    CategoryId = 1
                },
                new Book()
                {
                    Id = 1,
                    Name = "Book Test 2",
                    Author = "Author Test 2",
                    Description = "Description Test 2",
                    CategoryId = 1
                },
                new Book()
                {
                    Id = 1,
                    Name = "Book Test 3",
                    Author = "Author Test 3",
                    Description = "Description Test 3",
                    CategoryId = 2
                }
            };
        }

        private List<BookResponse> MapModelToBookResponse(List<Book> books)
        {
            var listBooks = new List<BookResponse>();

            foreach(var item in books)
            {
                var book = new BookResponse()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Author = item.Author,
                    Description = item.Description,
                    PublishDate = item.PublishDate,
                    CategoryId = item.CategoryId
                };
                listBooks.Add(book);
            }
            return listBooks;
        }
    }
}