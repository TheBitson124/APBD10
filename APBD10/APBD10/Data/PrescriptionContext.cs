using APBD10.Models;
using Microsoft.EntityFrameworkCore;
namespace APBD10.Data;

public class PrescriptionContext :DbContext
{
    protected PrescriptionContext()
    {
        
    }

    public PrescriptionContext(DbContextOptions options) : base(options)
    {
        
    }
    public virtual DbSet<Medicament> Medicament { get; set; }
    public virtual DbSet<Prescription> Prescription { get; set; }
    public virtual DbSet<Patient> Patients{ get; set; }
    public virtual DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public virtual DbSet<Doctor> Doctor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor(){IdDoctor = 1,FirstName="Jan",LastName="Kowalski",Email="test@email"},
            new Doctor(){IdDoctor = 2,FirstName="Ktos",LastName="Jakis",Email="aa@aa"}
        );
        modelBuilder.Entity<Patient>().HasData(
            new Patient(){IdPatient = 1,FirstName = "Pacjent",Birthdate = new DateTime(2000,1,1)}
        );
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription(){IdPrescription = 1,Date = new DateTime(2000,1,1),DueDate = new DateTime(2020,1,1),IdDoctor = 1,IdPatient = 1}
        );
        modelBuilder.Entity<PrescriptionMedicament>().HasData(
            new PrescriptionMedicament(){IdMedicament = 1,IdPrescription = 1,Details = "cos tam",Dose = 1}
        );
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament(){IdMedicament = 1,Description = "Medicament",Name = "Lek", Type = "Suplement"}
        );
        base.OnModelCreating(modelBuilder);
    }




}