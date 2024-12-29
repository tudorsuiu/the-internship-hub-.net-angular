using Microsoft.EntityFrameworkCore;
using TheInternshipHub.Server.Domain.Entities;

namespace TheInternshipHub.Server.Domain
{
    namespace SmartService.Domain
    {
        public class ApplicationDbContext : DbContext
        {
            public DbSet<USER> Users { get; set; }
            public DbSet<INTERNSHIP> Internships { get; set; }
            public DbSet<COMPANY> Companies { get; set; }
            public DbSet<APPLICATION> Applications { get; set; }

            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<APPLICATION>()
                    .HasOne(a => a.Internship)
                    .WithMany()
                    .HasForeignKey(a => a.AP_INTERNSHIP_ID)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<APPLICATION>()
                    .HasOne(a => a.Student)
                    .WithMany()
                    .HasForeignKey(a => a.AP_STUDENT_ID)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<INTERNSHIP>()
                    .HasOne(i => i.Company)
                    .WithMany()
                    .HasForeignKey(i => i.IN_COMPANY_ID)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<INTERNSHIP>()
                    .HasOne(i => i.Recruiter)
                    .WithMany()
                    .HasForeignKey(i => i.IN_RECRUITER_ID)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        }
    }
}
