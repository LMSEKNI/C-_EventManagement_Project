using System.ComponentModel.DataAnnotations;

public class EventViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Date Début is required.")]
    public DateTime DateDebut { get; set; }

    [Required(ErrorMessage = "Date Fin is required.")]
    public DateTime DateFin { get; set; }

    [Required(ErrorMessage = "Nombre Max is required.")]
    public int NombreMax { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Nom Event is required.")]
    public string NomEvent { get; set; }

    [Required(ErrorMessage = "Prix is required.")]
    public float Prix { get; set; }
}
