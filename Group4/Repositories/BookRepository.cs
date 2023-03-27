using Group4.Data;
using Group4.Models;
using Microsoft.EntityFrameworkCore;

namespace Group4.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Book>> GetOrderedBooksByTitleAndAvailability(string? titleSearch, string? availabilityFilter)
    {
        if (string.IsNullOrEmpty(titleSearch) && string.IsNullOrEmpty(availabilityFilter))
            return GetAllBooks();

        if (string.IsNullOrEmpty(titleSearch))
            return GetBooksByAvailability(availabilityFilter);

        if (string.IsNullOrEmpty(availabilityFilter))
            return GetBooksByTitle(titleSearch);

        return _context.Books
            .Where(book => book.Name.ToLower().Contains(titleSearch.ToLower()))
            .Where(book => availabilityFilter == "available"
                ? book.IsAvailable
                : availabilityFilter != "reserved" || !book.IsAvailable)
            .OrderByDescending(book => book.IsAvailable)
            .ThenBy(book => book.Name)
            .ToListAsync();
    }

    public Task<Book?> GetBookByIdOrNull(string id)
    {
        return _context.Books
            .FirstOrDefaultAsync(book => book.Id == id);
    }

    private Task<List<Book>> GetBooksByTitle(string titleSearch)
    {
        return _context.Books
            .Where(book => book.Name.ToLower().Contains(titleSearch.ToLower()))
            .OrderByDescending(book => book.IsAvailable)
            .ThenBy(book => book.Name)
            .ToListAsync();
    }

    private Task<List<Book>> GetBooksByAvailability(string? availabilityFilter)
    {
        return _context.Books
            .Where(book => availabilityFilter == "available"
                ? book.IsAvailable
                : availabilityFilter != "reserved" || !book.IsAvailable)
            .OrderBy(book => book.Name)
            .ToListAsync();
    }

    private Task<List<Book>> GetAllBooks()
    {
        return _context.Books
            .OrderByDescending(book => book.IsAvailable)
            .ThenBy(book => book.Name)
            .ToListAsync();
    }
}