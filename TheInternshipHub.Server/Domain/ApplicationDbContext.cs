using Microsoft.EntityFrameworkCore;

namespace TheInternshipHub.Server.Domain
{
    namespace SmartService.Domain
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}
