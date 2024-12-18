using TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;

namespace TestTask.Services.Implementations;

public class BookS :IBookService
{
    private readonly ApplicationDbContext _dbContext;

    public BookS(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book> GetBook()
    {
        return await _dbContext.Books
            .OrderBy(b => b.QuantityPublished * b.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Book>> GetBooks()
    {
        return await _dbContext.Books
            .Where(b => b.Title.Contains("Red") && b.PublishDate > new DateTime(2012, 6, 25))
            .ToListAsync();

    }
}