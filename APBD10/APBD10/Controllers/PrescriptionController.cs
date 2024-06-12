using System.Transactions;
using APBD10.Data;
using APBD10.Models;
using APBD10.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD10.Controllers;
[ApiController]

public class PrescriptionController :ControllerBase
{
    public IDbService Service;
    public PrescriptionContext Context;

    public PrescriptionController(IDbService service, PrescriptionContext context)
    {
        Service = service;
        Context = context;
    }

    [HttpPost]
    [Route("prescription")]
    public async Task<IActionResult> AddNewPrescription(AddPrescriptionDTO addPrescriptionDto)
    {
        var patient_id = addPrescriptionDto.Patient.IdPatient;
        var patient = await Service.DoesPatientExist(patient_id);
        
        try
        {
            await Service.DoAllMedicamentsExist(addPrescriptionDto.Medicaments);
        }
        catch (Exception e)
        {
            return NotFound("Medicament does not exist");
        }

        if (addPrescriptionDto.Medicaments.Count > 10)
        {
            return NotFound("More than 10 Medicaments");
        }

        if (addPrescriptionDto.DueDate < addPrescriptionDto.Date)
        {
            return NotFound("Date is past the DueDate");
        }

        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var p = Context.Prescriptions.Add(
                new Prescription()
                {
                    Date = addPrescriptionDto.Date,
                    DueDate = addPrescriptionDto.DueDate,
                    IdDoctor = 0,
                    IdPatient = addPrescriptionDto.Patient.IdPatient
                }
            );
            await Context.SaveChangesAsync();
            foreach (var medicament in addPrescriptionDto.Medicaments)
            {
                Context.PrescriptionMedicaments.Add(
                    new PrescriptionMedicament()
                    {
                        IdMedicament = medicament.id,
                        IdPrescription = p.Entity.IdPrescription,
                        Details = medicament.Description,
                        Dose = medicament.Dose
                    }
                );
                await Context.SaveChangesAsync();

            }
            if (patient == null)
            {
                Context.Patients.Add(
                    new Patient()
                    {
                        IdPatient = addPrescriptionDto.Patient.IdPatient,
                        FirstName = addPrescriptionDto.Patient.FirstName,
                        LastName = addPrescriptionDto.Patient.LastName,
                        Birthdate = addPrescriptionDto.Patient.Birthdate
                    }
                );
                await Context.SaveChangesAsync();
                scope.Complete();
            }
        }
        return Ok("Prescription Added");
    }

    [HttpGet]
    [Route("patient/{int:id}")]
    public async Task<IActionResult> GetPatientData(int id)
    {
        var patient = Context.Patients.Include(x => x.Prescriptions)
            .ThenInclude(x => x.PrescriptionMedicaments).ThenInclude(x => x.IdPrescriptionNavigation)
            .FirstOrDefault(x => x.IdPatient == id);
        if (patient == null)
        {
            return NotFound("Patient doesn't exist");
        }

        var ret_patient = new
        {
            patient.IdPatient,
            patient.FirstName,
            patient.LastName,
            patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(p => new
            {
                p.IdPrescription,
                p.Date,
                p.DueDate,
                PrescriptionMedicament = p.PrescriptionMedicaments.Select(x => new
                {
                    x.IdMedicament,
                    x.IdMedicamentNavigation.Name,
                    x.Dose,
                    x.Details
                }).ToList()
            }).ToList()
        };
        return Ok(ret_patient);
    }
}