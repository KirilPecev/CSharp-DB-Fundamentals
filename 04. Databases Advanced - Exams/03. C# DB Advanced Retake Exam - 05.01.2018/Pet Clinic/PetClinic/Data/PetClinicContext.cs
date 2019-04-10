namespace PetClinic.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PetClinicContext : DbContext
    {
        public PetClinicContext() { }

        public PetClinicContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalAid> AnimalAids { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<ProcedureAnimalAid> ProceduresAnimalAids { get; set; }
        public DbSet<Vet> Vets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProcedureAnimalAid>(e =>
            {
                e.HasKey(k => new { k.AnimalAidId, k.ProcedureId });
            });

            builder.Entity<Animal>(e =>
            {
                e.HasOne(p => p.Passport)
                    .WithOne(a => a.Animal);
            });

            builder.Entity<Procedure>(e =>
            {
                e.Ignore(c => c.Cost);
            });

            builder.Entity<AnimalAid>(e =>
            {
                e.HasIndex(i => i.Name)
                    .IsUnique();
            });
        }
    }
}
