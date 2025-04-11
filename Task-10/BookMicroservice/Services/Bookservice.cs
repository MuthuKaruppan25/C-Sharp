using BookMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using BookMicroservice.Data;
using BookMicroservice.Services;

namespace BookMicroservice.Services
{
    public class BookService : IBookService
    {
        private readonly BookDbContext bookDbContext;

        public BookService(BookDbContext context)
        {
            bookDbContext = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {

            var books = await bookDbContext.Books.ToListAsync();
            return books;
        }

        public async Task<Book> GetBookAsyncByID(int id)
        {
            var book = await bookDbContext.Books.SingleOrDefaultAsync(x => x.id == id);
            return book;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await bookDbContext.Books.AddAsync(book);
            await bookDbContext.SaveChangesAsync();
            return book;

        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            if (!bookDbContext.Books.Any(b => b.id == book.id)) return false;
            bookDbContext.Entry(book).State = EntityState.Modified;
            await bookDbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await bookDbContext.Books.FindAsync(id);
            if (book == null) return false;
            bookDbContext.Books.Remove(book);
            await bookDbContext.SaveChangesAsync();
            return true;
        }
    }

}
