using Microsoft.EntityFrameworkCore;

namespace Full_Stack_Gruppe_3.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Observation> Observations { get; set; }
    }
}
