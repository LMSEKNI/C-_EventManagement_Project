using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

public class Event
{
    [Key]
    public int Id { get; set; }
    [Required]

    public DateTime DateDebut { get; set; }
    [Required]

    public DateTime DateFin { get; set; }
    [Required]

    public int NombreMax { get; set; }
    [Required]

    public string Description { get; set; }

    [Required]
    public string Type { get; set; }
    [Required]
    public string NomEvent { get; set; }
    [Required]
    public float Prix { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Activity> Activities { get; set; }
}