﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Facillita.Users.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;

        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            IdentityUser<int> admin = new IdentityUser<int>
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 99999
            };

            PasswordHasher<IdentityUser<int>> hasher = new PasswordHasher<IdentityUser<int>>();

            //Secrets.json used for "adminpassword"
            admin.PasswordHash = hasher.HashPassword(admin, _configuration.GetValue<string>("adminpassword")); 

            builder.Entity<IdentityUser<int>>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99999, Name = "admin", NormalizedName = "ADMIN" }
                );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 99998, Name = "regular", NormalizedName = "REGULAR" }
                );  

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 99999, UserId = 99999 }
                );

        }
    }
}
