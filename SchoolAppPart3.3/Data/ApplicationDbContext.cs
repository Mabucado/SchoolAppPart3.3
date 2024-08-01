using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SchoolAppPart3._3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SchoolAppPart3._3.Models.Modules> Modules { get; set; } = default!;

        public DbSet<SchoolAppPart3._3.Models.Work>? Work { get; set; }
        public virtual DbSet<SchoolAppPart3._3.Models.VMLogin>? VMLogin { get; set; } = default!;
    }
}