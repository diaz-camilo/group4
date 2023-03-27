using Microsoft.AspNetCore.Identity;

namespace Group4.Models;

public class ApplicationUser : IdentityUser
{
    public virtual List<Reservation>? Reservations { get; set; }
}