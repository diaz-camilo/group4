using Group4.Models;
using Group4.Repositories;
using Group4.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group4.Controllers;

[Authorize]
public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IReservationRepository _reservationRepository;

    public BookController(IBookRepository bookRepository,
        IReservationRepository reservationRepository)
    {
        _bookRepository = bookRepository;
        _reservationRepository = reservationRepository;
    }

    // GET: Book
    [AllowAnonymous]
    public async Task<IActionResult> Index(string? titleSearch, string? availabilityFilter)
    {
        var books = await _bookRepository.GetOrderedBooksByTitleAndAvailability(titleSearch, availabilityFilter);

        return View(books);
    }


    // GET: Book/Reserve/5
    public async Task<IActionResult> Reserve(string id)
    {
        var book = await _bookRepository.GetBookByIdOrNull(id);

        if (book == null) return NotFound();

        return View(new ReservationViewModel {Book = book});
    }

    // POST: Book/ConfirmReserve
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmReserve([Bind("Id")] Book reservationBook)
    {
        var book = await _bookRepository.GetBookByIdOrNull(reservationBook.Id);

        if (book == null) return NotFound();

        if (!book.IsAvailable) return Problem("Book is not available");
        
        var reservation = await _reservationRepository.ReserveBook(User.Identity?.Name!, book);

        return reservation == null
            ? NotFound()
            : View("Reserve", new ReservationViewModel {Book = reservation.Book, Reservation = reservation});
    }

    public async Task<IActionResult> CancelReservation(int id)
    {
        var result = await _reservationRepository.CancelReservation(User.Identity?.Name!, id);

        return RedirectToAction("Index", "User", new {isCancelReservationSuccessful = result, reservationNumber = id});
    }
}