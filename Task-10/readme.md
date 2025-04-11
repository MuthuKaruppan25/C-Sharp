# 📚 Book Microservice (ASP.NET Core)

A minimal microservice built using **ASP.NET Core** that provides a RESTful API to manage books. It demonstrates core architectural concepts such as routing, dependency injection, async data access using Entity Framework Core, custom middleware, exception handling, and API documentation.

---

## 🚀 Objective

Build a small, well-structured RESTful API using ASP.NET Core that supports:

- Full CRUD operations on a resource (`Book`)
- Dependency Injection (DI) and Service Abstraction
- EF Core-based data access with MySQL
- Async programming
- Global exception logging using Middleware
- Custom action filters
- API documentation using OpenAPI (Swagger)

---

## 🛠️ Features & Technologies

| Feature                        | Description                                                                 |
|-------------------------------|-----------------------------------------------------------------------------|
| ✅ ASP.NET Core Web API        | Core framework for building RESTful endpoints                              |
| ✅ Entity Framework Core       | ORM used with MySQL (or in-memory/SQLite) for database access              |
| ✅ Dependency Injection        | All services are injected and managed using built-in DI container          |
| ✅ Asynchronous Operations     | Uses `async`/`await` for non-blocking database calls                       |
| ✅ Custom Middleware           | Logs request and response metadata globally                                |
| ✅ Action Filters              | Logs execution before and after controller actions                         |
| ✅ Swagger/OpenAPI             | Auto-generates API documentation                                            |

---

## 🧪 API Endpoints

| Method | Route              | Description                |
|--------|--------------------|----------------------------|
| GET    | `/api/book`        | Get all books              |
| GET    | `/api/book/{id}`   | Get book by ID             |
| POST   | `/api/book`        | Add a new book             |
| PUT    | `/api/book/{id}`   | Update an existing book    |
| DELETE | `/api/book/{id}`   | Delete a book by ID        |

Controller
```csharp
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

```
---

## 💾 Database Configuration

Database connection is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=booksdb;user=root;password=yourpassword"
  }
}
```

DbContext
```csharp
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
```

---

## 🧱 Core Components

```csharp
public class Book
{
    public int id { get; set; }
    public string? name { get; set; }
    public string? author { get; set; }
}
```

---

## 🔹 Service Interface & Implementation

Defines the IBookService interface and BookService for CRUD logic using EF Core.

IBookService.cs
```csharp
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
```

BookService.cs
```csharp
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
```

---

## 🔹 Middleware: Request Logging

Logs method, path, duration, and status code for every request.

```csharp
app.UseMiddleware<LogMiddleware>();
```

LoggingMiddleware.cs
```csharp
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BookMicroservice.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<LogMiddleware> _logger;

        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> _logger)
        {
            this.next = next;
            this._logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            var request = context.Request;
            _logger.LogInformation("Handling request: {method} {url}", request.Method, request.Path);

            await next(context);

            stopwatch.Stop();
            _logger.LogInformation("Finished request in {duration} ms with status code {statusCode}",
                stopwatch.ElapsedMilliseconds,
                context.Response.StatusCode);
        }

    }
}
```

---

## 🔹 Filter: Action Lifecycle Logging

Logs before and after every controller action using MyCustomFilter.

ActionFilter.cs
```csharp
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BookMicroservice.Middleware
{
    public class MyCustomFilter : IAsyncActionFilter
    {
        private readonly ILogger<MyCustomFilter> _logger;

        public MyCustomFilter(ILogger<MyCustomFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("➡️ MyCustomFilter executing before action: {Path}", context.HttpContext.Request.Path);
            var resultContext = await next();
            _logger.LogInformation("⬅️ MyCustomFilter executed after action");
        }
    }
}
```

---

## 🔹 API Documentation

Uses Scalar.AspNetCore for OpenAPI documentation.

```csharp
builder.Services.AddOpenApi();
app.MapOpenApi();  
```
