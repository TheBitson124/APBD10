using APBD10.Data;
using APBD10.Models;

namespace APBD10.Services;

public class DbService :IDbService

{
    public PrescriptionContext Context;

    public DbService(PrescriptionContext context)
    {
        Context = context;
    }

    public async Task<Patient?> DoesPatientExist(int id)
    {
        return await Context.Patients.FindAsync(id);
    }

    public async Task DoAllMedicamentsExist(List<ExistMedicamentDTO> medicamentDtos)
    {
        foreach (ExistMedicamentDTO medicament in medicamentDtos)
        {
            if (await Context.Medicaments.FindAsync(medicament.id) == null)
            {
                throw new Exception();
            }
        }
    }
}