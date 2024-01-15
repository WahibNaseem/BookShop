using AutoMapper;
using BookShop.API.Contracts.V1.Book.Requests;
using BookShop.API.Contracts.V1.Book.Responses;
using BookShop.Domain.Entities;
using BookShop.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.V1
{
    public class BooksController : MainController
    {
        private readonly IBookService _bookService;

        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        /// <summary>
        ///  helps you to get all the book
        /// </summary>
        /// <returns>return all the books with Statuscode 200 Success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BookResponse>>(books));
        }

        /// <summary>
        ///   help you get you the single book of the id you pass
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return a single book if exist or 404 not found if doesn't exist</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);

            if (book is null)
                return NotFound();

            return Ok(_mapper.Map<BookResponse>(book));
        }

        /// <summary>
        /// It helps you to get the books respective to the category 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>books of a category which user wants</returns>
        [HttpGet]
        [Route("get-books-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBooksById(int categoryId)
        {
            var books = await _bookService.GetBooksByCategoryAsync(categoryId);

            if (!books.Any())
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<BookResponse>>(books));
        }

        /// <summary>
        /// new books get created if it doesn't exist
        /// </summary>
        /// <param name="createBookRequest"></param>
        /// <returns>newly created book return if it gets created successfully</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(CreateBookRequest createBookRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var book = _mapper.Map<Book>(createBookRequest);
            var bookResult = await _bookService.AddAsync(book);

            if (bookResult is null)
                return BadRequest();

            return Ok(_mapper.Map<BookResponse>(bookResult));
        }

        /// <summary>
        ///   Modifed or updated the book  if it exist
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateBookRequest"></param>
        /// <returns>return the updated book </returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateBookRequest updateBookRequest)
        {
            if (id != updateBookRequest.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            await _bookService.UpdateAsync(_mapper.Map<Book>(updateBookRequest));

            return Ok(updateBookRequest);
        }

        /// <summary>
        /// Remove the book with the Id  being pass if exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return success reponse if the book been removed successfully</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.RemoveAsync(book);

            return Ok();
        }

        /// <summary>
        ///  it helps you to get the book with its name which being pass it that book exist
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns>it gets you the book which</returns>
        [HttpGet]
        [Route("search/{bookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> Search(string bookName)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchAsync(bookName));

            if (books == null || books.Count == 0)
                return NotFound("None book was founded");

            return Ok(books);
        }

        /// <summary>
        ///  it search the book with respective search key , It could be name , author, category
        /// </summary>
        /// <param name="searchKey"></param>
        /// <returns>it gets you the books</returns>
        [HttpGet]
        [Route("search-book-with-category/{searchKey}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Book>>> SearchBookWithCategory(string searchKey)
        {
            var books = _mapper.Map<List<Book>>(await _bookService.SearchBookWithCategoryAsync(searchKey));

            if (!books.Any())
                return NotFound("None book was founded");

            return Ok(_mapper.Map<IEnumerable<BookResponse>>(books));
        }

    }
}
