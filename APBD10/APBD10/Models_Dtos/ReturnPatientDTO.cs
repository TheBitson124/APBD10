namespace APBD10.Models;

public class ReturnPatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<Prescription> Prescriptions { get; set; }
    
}