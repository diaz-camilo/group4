using Group4.Data;
using Group4.Models;
using Microsoft.EntityFrameworkCore;

namespace Group4.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;

    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> ReserveBook(string userId, Book book)
    {
        var user = _context.Users.First(user => user.UserName == userId);
        var reservation = await _context.Reservations
            .AddAsync(new Reservation {Book = book, ApplicationUser = user});
        reservation.Entity.Book.IsAvailable = false;
        await _context.SaveChangesAsync();
        return reservation.Entity;
    }

    public Task<List<Book>> GetReservationsByUserId(string userId)
    {
        return _context.Books
            .Where(book => book.Reservation != null && book.Reservation.ApplicationUser.UserName == userId)
            .OrderBy(book => book.Name)
            .ToListAsync();
    }

    public Task<List<Book>> GetReservationsByUserIdFilteredByTitle(string userId, string titleSearch)
    {
        return _context.Books
            .Where(book => book.Reservation != null && book.Reservation.ApplicationUser.UserName == userId)
            .Where(book => book.Name.ToLower().Contains(titleSearch.ToLower()))
            .OrderBy(book => book.Name)
            .ToListAsync();
    }

    public async Task<bool> CancelReservation(string user, int id)
    {
        var reservation = await _context.Reservations
            .FirstOrDefaultAsync(res => res.Id == id);

        if (reservation == null || reservation.ApplicationUser.UserName != user) return false;

        reservation.Book.IsAvailable = true;
        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();

        return true;
    }
}