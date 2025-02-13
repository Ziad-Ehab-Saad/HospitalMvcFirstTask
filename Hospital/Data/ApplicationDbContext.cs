using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Doctor>doctors{ get; set; }
        public DbSet<Patient> patients{ get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FirstTaskMvcHospotal;Integrated Security=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>(e =>
            {
                e.Property(e => e.Name).HasMaxLength(50).IsUnicode(false).IsRequired();
                e.Property(e => e.Specialization).HasMaxLength(50).IsUnicode(false).IsRequired();
                e.HasKey(e => e.Id);
                e.HasMany(e => e.patients).WithOne(e => e.doctor).HasForeignKey(e => e.DoctorId);

            });
            modelBuilder.Entity<Patient>(e => {
                e.HasKey(e => e.id);
                e.Property(e => e.Name).HasMaxLength(50).IsRequired().IsUnicode(false);

            });

        }




    }
}
