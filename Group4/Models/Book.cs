using System.ComponentModel;

namespace Group4.Models;

public class Book
{
    public string Id { get; set; }

    [DisplayName("Book Title")] public string Name { get; set; }

    [DisplayName("Availability")] public bool IsAvailable { get; set; } = true;

    public virtual Reservation? Reservation { get; set; }
}