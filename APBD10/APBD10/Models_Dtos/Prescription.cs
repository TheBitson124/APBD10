using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace APBD10.Models;
[Table("prescription")]
public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public int IdPatient { get; set; }
    [Required]
    public int IdDoctor { get; set; }
    
    [ForeignKey(nameof(IdPatient))] public Patient IdPatientNavigation { get; set; }
    [ForeignKey(nameof(IdDoctor))] public Patient IdDoctorNavigation { get; set; }
    
    public List<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();


}