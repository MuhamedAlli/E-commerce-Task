using AssignmentForFullStack.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssignmentForFullStack.EF
{
    public class ApplicationDbContext: IdentityDbContext<AppUser>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasIndex(u=>u.Email).IsUnique();
            base.OnModelCreating(builder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
