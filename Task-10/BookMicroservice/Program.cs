using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using BookMicroservice.Data;
using BookMicroservice.Controllers;
using BookMicroservice.Services;
using Scalar.AspNetCore;
using BookMicroservice.Middleware;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<BookDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );

        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<MyCustomFilter>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
            app.MapOpenApi();
        }
        app.UseMiddleware<LogMiddleware>();


        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}

