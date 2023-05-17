using System.ComponentModel.DataAnnotations;

public class Activity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public TimeSpan HeureDebut { get; set; }
    [Required]

    public TimeSpan HeureFin { get; set; }
    [Required]

    public string NomActivity { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]

    public DateTime Date { get; set; }

    public  int EventId { get; set; }
    public virtual Event Event { get; set; }
}