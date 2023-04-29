using DayThree.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DayThree.Data.Context
{
    public class CompanyContext : IdentityDbContext <Employee>
    {

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().ToTable(nameof(Employee));
            builder.Entity<IdentityUserClaim<string>>().ToTable("EmployeeClaims");


        }
    }

}
