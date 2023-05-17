using System.ComponentModel.DataAnnotations;

public class ActivityInputModel
{
    [Required(ErrorMessage = "Heure Début is required.")]
    public TimeSpan HeureDebut { get; set; }

    [Required(ErrorMessage = "Heure Fin is required.")]
    public TimeSpan HeureFin { get; set; }

    [Required(ErrorMessage = "Nom Activity is required.")]
    public string NomActivity { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }
}
