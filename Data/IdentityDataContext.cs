using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using workshopproject.API.Models;

namespace workshopproject.API.Data
{
    public class IdentityDataContext : IdentityUserContext<User, int, IdentityUserClaim<int>, IdentityUserLogin<int>, IdentityUserToken<int>>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}