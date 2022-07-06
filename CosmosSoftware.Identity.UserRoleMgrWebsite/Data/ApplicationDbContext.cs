using AspNetCore.Identity.CosmosDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CosmosSoftware.Identity.UserRoleMgrWebsite.Models;

namespace CosmosSoftware.Identity.UserRoleMgrWebsite.Data
{
    /// <summary>
    /// Application Db Context
    /// </summary>
    public class ApplicationDbContext : CosmosIdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
          : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // DO NOT REMOVE THIS LINE. If you do, your context won't work as expected.
            base.OnModelCreating(builder);

            // TODO: Add your own fluent mappings
        }

        public DbSet<CosmosSoftware.Identity.UserRoleMgrWebsite.Models.IdentityUserViewItem>? IdentityUserViewItem { get; set; }
    }
}