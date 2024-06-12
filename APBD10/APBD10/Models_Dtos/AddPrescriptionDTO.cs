namespace APBD10.Models;

public class AddPrescriptionDTO
{
    public Patient Patient { get; set; }
    public List<ExistMedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
}