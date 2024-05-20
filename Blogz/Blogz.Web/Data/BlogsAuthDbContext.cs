using Blogz.Web.Models.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogz.Web.Data
{
    public class BlogsAuthDbContext : IdentityDbContext<ApplicationUser, Role, Guid>
    {
        public BlogsAuthDbContext(DbContextOptions<BlogsAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = Guid.Parse("db793edd-05be-4dc5-95e2-7d70c551bbe2");
            var adminRoleId = Guid.Parse("93dc99b9-301f-41a8-b3b7-fdb0381bf280");
            var userRoleId =  Guid.Parse("54693d31-64bf-426e-a009-652261fee407");

            var roles = new List<Role>  // Seed default Roles
            {
                new Role
                {
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId.ToString(),
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                },
                new Role
                {
                   Id = adminRoleId,
                   ConcurrencyStamp = adminRoleId.ToString(),
                   Name = "Admin",
                   NormalizedName = "Admin",
                },
                new Role
                {
                    Id =userRoleId,
                    ConcurrencyStamp = userRoleId.ToString(),
                    Name = "User",
                    NormalizedName = "User",
                },
            };

            builder.Entity<Role>().HasData(roles); // Insert roles in the database

            //Seed Super user
            var superAdminId = Guid.Parse("dea533e1-bb48-46a9-94b5-b8e44c2bde4f");

            var superAdminUser = new ApplicationUser
            {
                Id = superAdminId,
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                NormalizedEmail = "superadmin@gmail.com".ToUpper(),
                NormalizedUserName = "superadmin@gmail.com".ToUpper(),
                SecurityStamp = superAdminId.ToString(),    
            };

            superAdminUser.PasswordHash = new PasswordHasher<ApplicationUser>()
                .HashPassword(superAdminUser, "Superadmin@gmail1"); // Create password

            builder.Entity<ApplicationUser>().HasData(superAdminUser); // Insert super user in the database

            var superAdminRoles = new List<IdentityUserRole<Guid>>
            {
                new IdentityUserRole<Guid>
                {
                    
                    UserId = superAdminUser.Id,
                    RoleId = superAdminRoleId,
                },
                new IdentityUserRole<Guid>
                {
                    UserId = superAdminUser.Id,
                    RoleId = adminRoleId,
                },
                new IdentityUserRole<Guid>
                {
                    UserId = superAdminUser.Id,
                    RoleId = userRoleId,
                }
            };

            builder.Entity<IdentityUserRole<Guid>>().HasData(superAdminRoles);
        }
    }
}
