using TestTask.Models;
using TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using TestTask.Data;

namespace TestTask.Services.Implementations;

public class AuthorS : IAuthorService
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorS(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Author>> GetAuthors()
    {
        return await _dbContext.Authors
            .Where(a => _dbContext.Books.Count (b => b.AuthorId == a.Id && b.PublishDate > new DateTime(2015, 1, 1)) % 2 == 0)
            .ToListAsync();
    }

    public async Task<Models.Author> GetAuthor()
    {
        var bookTitelLength =  _dbContext.Books
            .OrderByDescending(b => b.Title.Length)
            .ThenBy(b => b.AuthorId)
            .FirstOrDefaultAsync();
        return await _dbContext.Authors
            .FirstOrDefaultAsync();
    }
}