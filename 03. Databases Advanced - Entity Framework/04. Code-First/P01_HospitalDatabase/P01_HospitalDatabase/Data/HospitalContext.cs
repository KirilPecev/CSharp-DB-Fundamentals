namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Visitation> Visitations { get; set; }

        public DbSet<Diagnose> Diagnoses { get; set; }

        public DbSet<Medicament> Medicaments { get; set; }

        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Visitation>(entity =>
            {
                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Visitations_Patients");
            });


            builder.Entity<Diagnose>(entity =>
            {
                entity.HasOne(e => e.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Diagnoses_Patients");
            });

            builder.Entity<PatientMedicament>(entity =>
                {
                    entity.HasKey(k => new { k.PatientId, k.MedicamentId });

                    entity.HasOne(e => e.Patient)
                        .WithMany(p => p.Prescriptions)
                        .HasForeignKey(e => e.PatientId)
                        .HasForeignKey("FK_PatientsMedicaments_Patients");

                    entity.HasOne(e => e.Medicament)
                        .WithMany(m => m.Prescriptions)
                        .HasForeignKey(m => m.MedicamentId)
                        .HasConstraintName("FK_PatientsMedicaments_Medicaments");
                }
            );

            builder.Entity<Doctor>(entity =>
            {
                entity.HasMany(e => e.Visitations)
                    .WithOne(e => e.Doctor)
                    .HasForeignKey(e => e.PatientId)
                    .HasConstraintName("FK_Doctors_Visitations");
            });
        }
    }
}
