using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using BookMicroservice.Data;

namespace BookMicroservice.Data
{
    public class BookDbContextFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();

            var connectionString = "server=localhost;port=3306;database=BookDb;user=bookuser;password=bookpass123";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new BookDbContext(optionsBuilder.Options);
        }
    }
}
