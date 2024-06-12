using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using APBD10.Models;
namespace APBD10.Models;

[Table("doctor")]
public class Doctor
{
    [Key] public int IdDoctor { get; set; }

    [MaxLength]
    [Required]
    public string FirstName { get; set; }
    [MaxLength]
    [Required]
    public string LastName { get; set; }
    [MaxLength]
    [Required]
    public string Email { get; set; }

    public List<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    
}