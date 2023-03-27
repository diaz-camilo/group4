using Group4.Models;

namespace Group4.ViewModels;

public class ReservationViewModel
{
    public Book Book { get; set; }
    public Reservation? Reservation { get; set; }
}