using APBD10.Models;

namespace APBD10.Services;

public interface IDbService
{
    Task<Patient?> DoesPatientExist(int id);
    Task DoAllMedicamentsExist(List<ExistMedicamentDTO> medicamentDtos);
}