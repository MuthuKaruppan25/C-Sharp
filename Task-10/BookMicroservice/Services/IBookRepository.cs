using BookMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using BookMicroservice.Data;

namespace BookMicroservice.Services{
    public interface IBookService {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book?> GetBookAsyncByID(int id);
        Task<Book?> AddBookAsync(Book book);

        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}