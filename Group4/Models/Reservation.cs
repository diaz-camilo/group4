using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Group4.Models;

public class Reservation
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Reservation Number")]
    public int Id { get; set; }

    public virtual string BookId { get; set; }
    public virtual Book Book { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    public bool IsActive { get; set; } = true;
}