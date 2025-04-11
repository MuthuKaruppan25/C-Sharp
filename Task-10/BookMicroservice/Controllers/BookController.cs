using BookMicroservice.Models;
using BookMicroservice.Services;
using Microsoft.AspNetCore.Mvc;
using BookMicroservice.Middleware;
namespace BookMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        [ServiceFilter(typeof(MyCustomFilter))]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await bookService.GetBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBooksbyId(int id)
        {
            var book = await bookService.GetBookAsyncByID(id);
            if (book is null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            if (book is null)
            {
                return BadRequest();
            }

            var added = await bookService.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBooksbyId), new { id = added.id }, added);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.id)
                return BadRequest();

            var updated = await bookService.UpdateBookAsync(book);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var deleted = await bookService.DeleteBookAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
