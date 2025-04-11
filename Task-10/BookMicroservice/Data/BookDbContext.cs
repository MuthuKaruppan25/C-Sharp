using BookMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace BookMicroservice.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(
            DbContextOptions<BookDbContext> options
        ) : base(options)
        {

        }
        public DbSet<Book> Books => Set<Book>();
    }
}