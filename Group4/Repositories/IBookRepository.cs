using Group4.Models;

namespace Group4.Repositories;

public interface IBookRepository
{
    Task<List<Book>> GetOrderedBooksByTitleAndAvailability(string? titleSearch, string? availabilityFilter);
    Task<Book?> GetBookByIdOrNull(string id);
}