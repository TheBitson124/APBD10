using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Models;

[Table("prescriptionmedicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]  

public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }

    public int IdPrescription { get; set; }
    
    public int Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; } = null!;

    [ForeignKey(nameof(IdMedicament))] public Medicament IdMedicamentNavigation { get; set; } = null!;

    [ForeignKey(nameof(IdPrescription))] public Prescription IdPrescriptionNavigation { get; set; } = null!;
}