using Group4.Models;

namespace Group4.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> ReserveBook(string user, Book book);
    Task<bool> CancelReservation(string user, int id);
    Task<List<Book>> GetReservationsByUserId(string userId);
    Task<List<Book>> GetReservationsByUserIdFilteredByTitle(string userId, string titleSearch);
}