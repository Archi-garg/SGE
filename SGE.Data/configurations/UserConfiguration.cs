using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGE.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Data.configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User()
                {
                    Id = "1cfdbb18-b4f8-40c1-9d79-0498d2001c32",
                    UserName = "Admin",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                    NormalizedEmail = "Admin@gmail.com",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123"),
                    Image = "https://localhost:7148/Image/ProfilePic.jpg"

                },
                new User()
                {
                    Id = "8c1e9f47-a2b2-43a2-8a23-53a9710e3248",
                    UserName = "User",
                    FirstName = "User",
                    LastName = "User",
                    Email = "User@gmail.com",
                    PhoneNumber = "1234567890",
                    EmailConfirmed = true,
                    NormalizedEmail = "User@gmail.com",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "User123"),
                    Image = "https://localhost:7148/Image/ProfilePic.jpg"

                });
        }
    }
}
