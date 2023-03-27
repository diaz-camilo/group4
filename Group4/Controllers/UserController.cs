using Group4.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group4.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IReservationRepository _reservationRepository;

    public UserController(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    // GET: User?titleSearch={titleSearch}
    public async Task<IActionResult> Index(string? titleSearch)
    {
        return View(string.IsNullOrWhiteSpace(titleSearch)
            ? await _reservationRepository.GetReservationsByUserId(User.Identity?.Name!)
            : await _reservationRepository.GetReservationsByUserIdFilteredByTitle(User.Identity?.Name!, titleSearch)
        );
    }
}