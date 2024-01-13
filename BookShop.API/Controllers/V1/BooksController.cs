using BookShop.API.Contracts.V1;
using BookShop.API.Data;
using BookShop.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.API.Controllers.V1
{
    public class BooksController : MainController
    {
        private readonly BookShopDbContext _bookShopDbContext;

        public BooksController(BookShopDbContext bookShopDbContext)
        {
            _bookShopDbContext=bookShopDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _bookShopDbContext.Books.ToList();
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var book = _bookShopDbContext.Books.Find(id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Book book)
        {
            _bookShopDbContext.Add(book);
            _bookShopDbContext.SaveChanges();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Remove([FromRoute] int id)
        {
            var bookToRemove = _bookShopDbContext.Books.Find(id);
            _bookShopDbContext.Books.Remove(bookToRemove);
            _bookShopDbContext.SaveChanges();

            return Ok();
        }
    }
}
