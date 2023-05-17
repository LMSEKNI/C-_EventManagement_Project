using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Cin { get; set; }
    [Required]
    public string Nom { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public virtual ICollection<Event> Events { get; set; }
}